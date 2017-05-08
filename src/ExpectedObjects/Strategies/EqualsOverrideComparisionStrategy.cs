using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ExpectedObjects.Strategies
{
    public class EqualsOverrideComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            if (IsAnonymousType(type))
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

        public bool IsAnonymousType(Type type)
        {
            var hasCompilerGeneratedAttribute =
                type.GetTypeInfo().GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any();
            var nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            var isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;

            return isAnonymousType;
        }
    }
}