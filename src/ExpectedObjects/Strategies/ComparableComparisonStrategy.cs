using System;

namespace ExpectedObjects.Strategies
{
    public class ComparableComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(object expected, object actual)
        {
            return expected is IComparable && actual is IComparable;
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            try
            {
                return ((IComparable) expected).CompareTo(actual) == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}