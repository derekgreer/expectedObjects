﻿using System;
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

        readonly StackDictionary<object, IComparisonResult> _visited =
            new StackDictionary<object, IComparisonResult>();

        bool _ignoreTypeInformation;
        IWriter _writer;
        object _expected;

        public EqualityComparer(IConfiguration configurationContext)
        {
            _configurationContext = configurationContext;
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

        IComparison GetMemberComparison(string path)
        {
            var expectedRootType = _expected.GetType();
            
            if(_ignoreTypeInformation)
                path =  string.Join(".", new [] { expectedRootType.Name, string.Join(".", path.Split('.').Skip(1))}.Where(x => !string.IsNullOrEmpty(x)));
            var strategy = _configurationContext.MemberStrategies.LastOrDefault(s => s.ShouldApply(path));

            return strategy?.Comparison;
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
                var areEqual = true;
                _elementStack.Push(member);


                if (expected != null)
                {
                    var memberComparison = GetMemberComparison(GetMemberPath());

                    if (memberComparison != null)
                        return EvalComparison(actual, writer, memberComparison, GetMemberPath());
                }

                if (ReferenceEquals(expected, actual))
                {
                    if (!string.IsNullOrEmpty(member))
                        writer.Write(new EqualityResult(true, GetMemberPath(), expected, actual));

                    return true;
                }

                if (expected == null)
                {
                    writer.Write(new EqualityResult(false, GetMemberPath(), null, actual));
                    return false;
                }

                if (expected is IComparison comparison)
                    return EvalComparison(actual, writer, comparison, GetMemberPath());

                if (actual is IMissing)
                {
                    writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                    return false;
                }

                if (!_ignoreTypeInformation && !expected.GetType().IsInstanceOfType(actual))
                {
                    writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                    return false;
                }

                if (_ignoreTypeInformation && IsLeaf(expected) && !expected.GetType().IsInstanceOfType(actual))
                {
                    writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));
                    return false;
                }

                if (actual == null)
                {
                    writer.Write(new EqualityResult(false, GetMemberPath(), expected, null));
                    return false;
                }

                foreach (var strategy in _configurationContext.Strategies)
                    if (strategy.CanCompare(expected, actual))
                    {
                        areEqual = strategy.AreEqual(expected, actual, this);

                        if (!areEqual)
                            if (_elementStack.Count > 0)
                                writer.Write(new EqualityResult(false, GetMemberPath(), expected, actual));

                        break;
                    }

                if (areEqual) writer.Write(new EqualityResult(areEqual, GetMemberPath(), expected, actual));
                return areEqual;
            }
            finally
            {
                if (_elementStack.Count > 0)
                    _elementStack.Pop();
            }
        }

        bool EvalComparison(object actual, IWriter writer, IComparison comparison, string memberPath)
        {
            if (actual != null && actual is IMissing)
            {
                writer.Write(new EqualityResult(false, memberPath,
                    new ExpectedDescription(comparison.GetExpectedResult()),
                    actual));
                return false;
            }

            var areEqual = comparison.AreEqual(actual);

            if (!areEqual)
                writer.Write(new EqualityResult(false, memberPath,
                    new ExpectedDescription(comparison.GetExpectedResult()),
                    actual));

            return areEqual;
        }

        bool IsLeaf(object expected)
        {
            const BindingFlags propertyFlags = BindingFlags.Public | BindingFlags.Instance;
            var expectedPropertyInfos = expected.GetType()
                .GetVisibleProperties(propertyFlags);

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