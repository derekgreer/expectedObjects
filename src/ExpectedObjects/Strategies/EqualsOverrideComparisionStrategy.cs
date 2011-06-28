using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ExpectedObjects
{
    public class EqualsOverrideComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            if (IsAnonymousType(type))
            {
                return false;
            }

            MethodInfo mi = type.GetMethod("Equals",
                                           BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly,
                                           null, new[] {typeof (object)}, null);

            return (mi != null &&
                    ((mi.Attributes & MethodAttributes.Virtual) != 0) &&
                    ((mi.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.ReuseSlot));
        }


        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            return expected.Equals(actual);
        }

        public bool IsAnonymousType(Type type)
        {
            bool hasCompilerGeneratedAttribute =
                type.GetCustomAttributes(typeof (CompilerGeneratedAttribute), false).Length > 0;
            bool nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            bool isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;

            return isAnonymousType;
        }
    }
}