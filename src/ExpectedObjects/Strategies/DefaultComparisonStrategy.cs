using System;

namespace ExpectedObjects
{
    public class DefaultComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            return true;
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            return expected.Equals(actual);
        }
    }
}