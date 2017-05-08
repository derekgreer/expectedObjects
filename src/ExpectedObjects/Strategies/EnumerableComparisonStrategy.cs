using System;
using System.Collections;
using System.Reflection;

namespace ExpectedObjects.Strategies
{
    public class EnumerableComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            return (typeof (IEnumerable).IsAssignableFrom(type));
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            bool areEqual = true;
            var expectedEnumerable = (IEnumerable)expected;
            var actualEnumerable = (IEnumerable)actual;
            IEnumerator expectedEnumerator = expectedEnumerable.GetEnumerator();
            IEnumerator actualEnumerator = actualEnumerable.GetEnumerator();
            bool expectedHasValue = expectedEnumerator.MoveNext();
            bool actualHasValue = actualEnumerator.MoveNext();

            int yield = 0;

            while (expectedHasValue && actualHasValue)
            {
                areEqual = comparisonContext.AreEqual(expectedEnumerator.Current, actualEnumerator.Current, "[" + yield++ + "]") && areEqual;
                
                expectedHasValue = expectedEnumerator.MoveNext();
                actualHasValue = actualEnumerator.MoveNext();
            }

            if (!expectedHasValue && actualHasValue)
            {
                areEqual = comparisonContext.AreEqual(null, new UnexpectedElement(actualEnumerator.Current), "[" + yield + "]") &&
                areEqual;
            }
            else if (expectedHasValue)
            {
                areEqual = comparisonContext.AreEqual(expectedEnumerator.Current, new MissingElement(), "[" + yield + "]") &&
                areEqual;
            }

            return areEqual;
        }
    }
}