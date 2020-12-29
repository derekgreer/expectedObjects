using System.Linq;
using System.Reflection;

namespace ExpectedObjects.Strategies
{
    public class EqualsOverrideComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(object expected, object actual)
        {
            var expectedType = expected.GetType();
            var actualType = actual.GetType();

            if (expectedType.IsAnonymousType() || actualType.IsAnonymousType())
                return false;

            var expectedOverriddenEquals = expectedType
                .GetTypeInfo()
                .GetDeclaredMethods("Equals")
                .FirstOrDefault(m => m.IsVirtual && (m.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.ReuseSlot);

            var actualOverriddenEquals = actualType
                .GetTypeInfo()
                .GetDeclaredMethods("Equals")
                .FirstOrDefault(m => m.IsVirtual && (m.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.ReuseSlot);

            return expectedOverriddenEquals != null || actualOverriddenEquals != null;
        }


        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            return expected.Equals(actual);
        }
    }
}