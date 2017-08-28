using System.Collections.Generic;
using ExpectedObjects.Reporting;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public static class ExpectedObjectExtensions
    {
        public static ExpectedObject ToExpectedObject(this object expected)
        {
            return new ExpectedObjectBuilder()
                .UsingInstance(expected)
                .UsingStrategies(new List<IComparisonStrategy>
                {
                    new DefaultComparisonStrategy(),
                    new KeyValuePairComparisonStrategy(),
                    new ClassComparisonStrategy(),
                    new EnumerableComparisonStrategy(),
                    new EqualsOverrideComparisonStrategy(),
                    new PrimitiveComparisonStrategy(),
                    new ComparableComparisonStrategy()
                })
                .Build();
        }

        public static bool Matches(this ExpectedObject expected, object actual)
        {
            return expected.Equals(actual, new NullWriter(), true);
        }

        public static bool DoesNotMatch(this ExpectedObject expected, object actual)
        {
            return !expected.Equals(actual, new NullWriter(), true);
        }
    }
}