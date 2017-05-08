using System;
using System.Reflection;

namespace ExpectedObjects.Strategies
{
    public class PrimitiveComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
#if NET40
            return type.IsPrimitive;
#else
            return type.GetTypeInfo().IsPrimitive;
#endif
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            return expected.Equals(actual);
        }
    }
}