using System;
using System.Linq;
using System.Reflection;

namespace ExpectedObjects.Chain.Links
{
    class LeafInstanceOfTypeComparisonLink: IComparisonLink
    {
        public LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next)
        { 
            var expected = linkComparisonContext.Expected;
            var actual = linkComparisonContext.Actual;

            if (linkComparisonContext.IgnoreTypeInformation && IsLeaf(expected) && !expected.GetType().IsInstanceOfType(actual))
            {
                return LinkComparisonResult.False;
            }

            return next(linkComparisonContext);
        }

        bool IsLeaf(object expected)
        {
            if (expected == null)
                return true;

            const BindingFlags propertyFlags = BindingFlags.Public | BindingFlags.Instance;
            var expectedPropertyInfos = expected.GetType()
                .GetVisibleProperties(propertyFlags);

            var expectedFieldInfos = expected.GetType().GetFields(propertyFlags);

            return !expectedPropertyInfos.Any() && !expectedFieldInfos.Any();
        }
    }
}