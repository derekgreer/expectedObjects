using System;
using System.Reflection;

namespace ExpectedObjects.Strategies
{
    public class ComparableComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type);
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            return ((IComparable) expected).CompareTo(actual) == 0;
        }
    }
}