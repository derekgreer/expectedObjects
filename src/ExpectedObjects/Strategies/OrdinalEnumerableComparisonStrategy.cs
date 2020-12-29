using System.Collections;

namespace ExpectedObjects.Strategies
{
    public class OrdinalEnumerableComparisonStrategy : IComparisonStrategy
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
            var expectedEnumerator = expectedEnumerable.GetEnumerator();
            var actualEnumerator = actualEnumerable.GetEnumerator();
            var expectedHasValue = expectedEnumerator.MoveNext();
            var actualHasValue = actualEnumerator.MoveNext();

            var yield = 0;

            while (expectedHasValue && actualHasValue)
            {
                areEqual = comparisonContext.ReportEquality(expectedEnumerator.Current, actualEnumerator.Current,
                    $"[{yield++}]") && areEqual;

                expectedHasValue = expectedEnumerator.MoveNext();
                actualHasValue = actualEnumerator.MoveNext();
            }

            if (!expectedHasValue && actualHasValue)
                areEqual = comparisonContext.ReportEquality(null, new UnexpectedElement(actualEnumerator.Current),
                    $"[{yield}]") && areEqual;

            else if (expectedHasValue)
                areEqual = comparisonContext.ReportEquality(expectedEnumerator.Current, new MissingElement(),
                    $"[{yield}]") && areEqual;

            return areEqual;
        }
    }
}