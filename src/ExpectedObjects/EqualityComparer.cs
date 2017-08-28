using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ExpectedObjects.Reporting;

namespace ExpectedObjects
{
    public class EqualityComparer : IComparisonContext
    {
        readonly IConfiguration _configurationContext;
        readonly Stack<string> _elementStack = new Stack<string>();

        readonly StackDictionary<object, IComparisionResult> _visited =
            new StackDictionary<object, IComparisionResult>();

        bool _ignoreTypeInformation;

        public EqualityComparer(IConfiguration configurationContext)
        {
            _configurationContext = configurationContext;
        }

        public bool AreEqual(object expected, object actual, string member)
        {
            using (new WriterContext(new NullWriter()))
            {
                if (actual != null && _visited.ContainsKey(actual))
                    return _visited[actual].Result;

                if (actual != null)
                    _visited.Push(actual, new ComparisonResult(actual, true));

                var result = Compare(expected, actual, member, WriterContext.Current);

                if (actual != null)
                    _visited.Pop(actual);

                return result;
            }
        }

        public bool ReportEquality(object expected, object actual, string member)
        {
            if (actual != null && _visited.ContainsKey(actual))
                return _visited[actual].Result;

            if (actual != null)
                _visited.Push(actual, new ComparisonResult(actual, true));

            var result = Compare(expected, actual, member, WriterContext.Current);

            if (actual != null)
                _visited.Pop(actual);

            return result;
        }

        public bool CompareProperties(object expected, object actual,
            Func<PropertyInfo, PropertyInfo, bool> propertyComparison)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var expectedPropertyInfos = expected.GetType().GetProperties(flags)
                .ExcludeHiddenProperties(expected.GetType());
            var actualPropertyInfos = actual.GetType().GetProperties(flags).ExcludeHiddenProperties(expected.GetType());
            var areEqual = true;
            expectedPropertyInfos.ToList().ForEach(pi =>
            {
                areEqual = propertyComparison(pi, actualPropertyInfos .SingleOrDefault(p => p.Name.Equals(pi.Name))) & areEqual;
            });

            return areEqual;
        }

        public bool CompareFields(object expected, object actual, Func<FieldInfo, FieldInfo, bool> comparison)
        {
            var flags = _configurationContext.GetFieldBindingFlags();
            var expectedFieldInfos = expected.GetType().GetFields(flags);
            var actualFieldInfos = actual.GetType().GetFields(flags);
            var areEqual = true;
            expectedFieldInfos.ToList().ForEach(pi =>
            {
                areEqual = comparison(pi,
                               actualFieldInfos.SingleOrDefault
                                   (p => p.Name.Equals(pi.Name))) & areEqual;
            });

            return areEqual;
        }

        public void Report(bool status, string message, object expected, object actual)
        {
            WriterContext.Current.Write(new EqualityResult(status, GetMemberPath(), expected, actual,
                EqualityResultType.Custom, message));
        }


        public bool AreEqual(object expected, object actual, IWriter writer, bool ignoreTypeInformation = false)
        {
            _ignoreTypeInformation = ignoreTypeInformation;

            using (new WriterContext(writer))
            {
                return ReportEquality(expected, actual, actual?.GetType().Name ?? string.Empty);
            }
        }


        bool Compare(object expected, object actual, string member, IWriter writer)
        {
            try
            {
                var areEqual = true;
                _elementStack.Push(member);

                if (ReferenceEquals(expected, actual))
                {
                    if (!string.IsNullOrEmpty(member))
                        writer.Write(new EqualityResult(true, GetMemberPath(), expected, actual));

                    return true;
                }

                if (expected == null && actual != null)
                {
                    writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                    return false;
                }

                if (_ignoreTypeInformation)
                {
                    var comparison = expected as IComparison;

                    if (comparison != null)
                    {
                        if (actual != null && actual is IMissing)
                        {
                            writer.Write(new EqualityResult(false, GetMemberPath(),
                                new ExpectedDescription(comparison.GetExpectedResult()),
                                actual));
                            return false;
                        }

                        areEqual = comparison.AreEqual(actual);

                        if (!areEqual)
                            writer.Write(new EqualityResult(false, GetMemberPath(),
                                new ExpectedDescription(comparison.GetExpectedResult()),
                                actual));

                        return areEqual;
                    }

                    if (actual is IMissing)
                    {
                        writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                        return false;
                    }
                    else if (IsLeaf(expected) && !expected.GetType().IsInstanceOfType(actual))
                    {
                        writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                        return false;
                    }
                }
                else
                {
                    if (expected != null && !expected.GetType().IsInstanceOfType(actual))
                    {
                        writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                        return false;
                    }
                }

                if (actual == null)
                {
                    writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                    return false;
                }


                foreach (var strategy in _configurationContext.Strategies)
                    if (strategy.CanCompare(expected.GetType()))
                    {
                        areEqual = strategy.AreEqual(expected, actual, this);

                        if (!areEqual)
                            if (_elementStack.Count > 0)
                                writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));

                        break;
                    }

                if(areEqual) writer.Write(new EqualityResult(areEqual, GetMemberPath(), expected, actual));
                return areEqual;
            }
            finally
            {
                if (_elementStack.Count > 0)
                    _elementStack.Pop();
            }
        }

        bool IsLeaf(object expected)
        {
            const BindingFlags propertyFlags = BindingFlags.Public | BindingFlags.Instance;
            var expectedPropertyInfos = expected.GetType()
                .GetProperties(propertyFlags)
                .ExcludeHiddenProperties(expected.GetType());

            var fieldFlags = _configurationContext.GetFieldBindingFlags();
            var expectedFieldInfos = expected.GetType().GetFields(propertyFlags);

            return !expectedPropertyInfos.Any() && !expectedFieldInfos.Any();
        }

        string GetMemberPath()
        {
            var sb = new StringBuilder();

            sb.Append(string.Join(".", _elementStack.Reverse().ToArray()));
            sb.Replace(".[", "[");
            return sb.ToString();
        }
    }
}