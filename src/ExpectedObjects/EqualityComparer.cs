using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ExpectedObjects.Chain;
using ExpectedObjects.Chain.Links;
using ExpectedObjects.Reporting;

namespace ExpectedObjects
{
    public class EqualityComparer : IComparisonContext
    {
        readonly IConfiguration _configurationContext;
        readonly Stack<string> _elementStack = new Stack<string>();
        readonly StackDictionary<object, IComparisonResult> _visited = new StackDictionary<object, IComparisonResult>();
        bool _ignoreTypeInformation;
        IWriter _writer;
        object _expected;
        StrategyChain _strategyChain;

        public EqualityComparer(IConfiguration configurationContext)
        {
            _configurationContext = configurationContext;
            InitializeStrategyChain();
        }

        void InitializeStrategyChain()
        {
            _strategyChain = new StrategyChain();
            _strategyChain.Link(new TypeStrategyComparisonLink());
            _strategyChain.Link(new MemberComparisonLink());
            _strategyChain.Link(new ReferenceEqualsComparisonLink());
            _strategyChain.Link(new NullExpectedComparisonLink());
            _strategyChain.Link(new ComparisonComparisonLink());
            _strategyChain.Link(new MissingComparisonLink());
            _strategyChain.Link(new InstanceOfTypeComparisonLink());
            _strategyChain.Link(new LeafInstanceOfTypeComparisonLink());
            _strategyChain.Link(new NullActualComparisonLink());
            _strategyChain.Link(new StrategyComparisonLink());
            _strategyChain.Link(new TrueLink());
        }

        public bool AreEqual(object expected, object actual, string member)
        {
            var existingWriter = _writer;
            try
            {
                _writer = new NullWriter();
                if (actual != null && _visited.ContainsKey(actual))
                    return _visited[actual].Result;

                if (actual != null)
                    _visited.Push(actual, new ComparisonResult(actual, true));

                var result = Compare(expected, actual, member, _writer);

                if (actual != null)
                    _visited.Pop(actual);

                return result;
            }
            finally
            {
                _writer = existingWriter;
            }
        }

        public bool ReportEquality(object expected, object actual, string member)
        {
            if (actual != null && _visited.ContainsKey(actual))
                return _visited[actual].Result;

            if (actual != null)
                _visited.Push(actual, new ComparisonResult(actual, true));

            var result = Compare(expected, actual, member, _writer);

            if (actual != null)
                _visited.Pop(actual);

            return result;
        }

        public bool CompareProperties(object expected, object actual, Func<PropertyInfo, PropertyInfo, bool> propertyComparison)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var expectedPropertyInfos = expected.GetType().GetVisibleProperties(flags);
            var actualPropertyInfos = actual.GetType().GetVisibleProperties(flags);
            var areEqual = true;

            expectedPropertyInfos.ToList().ForEach(propertyInfo =>
            {
                try
                {
                    areEqual = propertyComparison(propertyInfo,
                        actualPropertyInfos.SingleOrDefault(p => p.Name.Equals(propertyInfo.Name) &&
                                                                 p.HasSameIndexParameters(propertyInfo))) && areEqual;
                }
                catch (Exception e)
                {
                    var res = actualPropertyInfos.Where(p => p.Name.Equals(propertyInfo.Name) && p.HasSameIndexParameters(propertyInfo));
                    Console.WriteLine(e);
                    throw;
                }
            });

            return areEqual;
        }

        public bool CompareFields(object expected, object actual, Func<FieldInfo, FieldInfo, bool> comparison)
        {
            var flags = _configurationContext.GetFieldBindingFlags();
            var expectedFieldInfos = expected.GetType().GetFields(flags);
            var actualFieldInfos = actual.GetType().GetFields(flags);
            var areEqual = true;
            expectedFieldInfos.ToList().ForEach(fieldInfo =>
            {
                areEqual = comparison(fieldInfo, actualFieldInfos.SingleOrDefault(f => f.Name.Equals(fieldInfo.Name))) && areEqual;
            });

            return areEqual;
        }

        public void Report(bool status, string message, object expected, object actual)
        {
            _writer.Write(new EqualityResult(status, GetMemberPath(), expected, actual,
                EqualityResultType.Custom, message));
        }

        public bool AreEqual(object expected, object actual, IWriter writer, bool ignoreTypeInformation = false)
        {
            _expected = expected;
            _writer = writer;
            _ignoreTypeInformation = ignoreTypeInformation;
            return ReportEquality(expected, actual, actual?.GetType().ToUsefulTypeName() ?? string.Empty);
        }

        bool Compare(object expected, object actual, string member, IWriter writer)
        {
            try
            {
                _elementStack.Push(member);

                var linkContext = new LinkComparisonContext
                {
                    Expected = expected,
                    Actual =  actual,
                    ComparisonContext = this,
                    MemberPath = GetExpectedMemberPath(),
                    Configuration = _configurationContext,
                    IgnoreTypeInformation = _ignoreTypeInformation
                };

                var result = _strategyChain.Process(linkContext);

                if (_elementStack.Count > 0)
                    writer.Write(new EqualityResult(result.Result, GetMemberPath(), result.ExpectedResult ?? expected, actual));
                return result.Result;
            }
            finally
            {
                if (_elementStack.Count > 0)
                    _elementStack.Pop();
            }
        }

        string GetExpectedMemberPath()
        {
            var memberPath = GetMemberPath();
            if (_expected != null)
                memberPath = ReplaceRootName(memberPath, _expected.GetType().Name);

            return memberPath;
        }

        static string ReplaceRootName(string path, string rootName)
        {
            return string.Join(".", new [] { rootName, string.Join(".", path.Split('.').Skip(1))}.Where(x => !string.IsNullOrEmpty(x)));
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