using System;
using System.Reflection;

namespace ExpectedObjects.Strategies
{
    public class PrimitiveComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(object expected, object actual)
        {
#if NET40
            return expected.GetType().IsPrimitive;
#else
            return expected.GetType().GetTypeInfo().IsPrimitive;
#endif
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            return expected.Equals(actual);
        }
    }
}