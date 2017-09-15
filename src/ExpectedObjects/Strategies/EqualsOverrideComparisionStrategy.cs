using System;
using System.Linq;
using System.Reflection;

namespace ExpectedObjects.Strategies
{
    public class EqualsOverrideComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            if (type.IsAnonymousType())
                return false;

            var overriddenEquals = type
                .GetTypeInfo()
                .GetDeclaredMethods("Equals")
                .FirstOrDefault(m => m.IsVirtual && (m.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.ReuseSlot);

            return overriddenEquals != null;
        }


        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            return expected.Equals(actual);
        }
    }
}