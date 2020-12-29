using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExpectedObjects.Strategies
{
    public class EnumerableComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(object expected, object actual)
        {
            return expected is IEnumerable && actual is IEnumerable;
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            var areEqual = true;
            var expectedEnumerable = (IEnumerable) expected;
            var actualEnumerable = (IEnumerable) actual;
            var expectedList = expectedEnumerable.Cast<object>().ToList();
            var actualList = actualEnumerable.Cast<object>().ToList();
            var expectedDictionary = GetDictionary(expectedList);
            var actualDictionary = GetDictionary(actualList);

            Action emptyDelegate = () => { };
            Action removeDelegate;

            foreach (var expectedEntry in expectedDictionary.ToList())
            {
                removeDelegate = emptyDelegate;
                if (!actualDictionary.Any(actualEntry =>
                {
                    if (!comparisonContext.AreEqual(expectedEntry.Value, actualEntry.Value, $"[{expectedEntry.Key}]")) return false;
                    removeDelegate = () => actualDictionary.Remove(actualEntry.Key);
                    return true;
                }))
                    areEqual = comparisonContext.ReportEquality(expectedEntry.Value, new MissingElement(), $"[{expectedEntry.Key}]") && areEqual;

                removeDelegate();
            }


            var unexpectedValues = new List<object>();
            for (var actualIndex = 0; actualIndex < actualList.Count; actualIndex++)
            {
                removeDelegate = emptyDelegate;

                if (!expectedDictionary.Any(expectedEntry =>
                {
                    var equal = comparisonContext.AreEqual(expectedEntry.Value, actualList[actualIndex], $"[{expectedEntry.Key}]");

                    if (equal)
                        removeDelegate = () => expectedDictionary.Remove(expectedEntry.Key);

                    return equal;
                }))
                    unexpectedValues.Add(actualList[actualIndex].ToObjectString());

                removeDelegate();
            }

            if (unexpectedValues.Any())
            {
                var unexpectedElements = string.Join($",{Environment.NewLine}{Environment.NewLine}", unexpectedValues.Select(s => $"{s}"));
                comparisonContext.Report(false, $"The following elements were unexpected:{Environment.NewLine}{Environment.NewLine}{unexpectedElements}", null,
                    null);
                areEqual = false;
            }
            return areEqual;
        }

        static Dictionary<int, object> GetDictionary(List<object> list)
        {
            var dictionary = new Dictionary<int, object>();
            for (var index = 0; index < list.Count; index++)
                dictionary.Add(index, list[index]);

            return dictionary;
        }
    }
}