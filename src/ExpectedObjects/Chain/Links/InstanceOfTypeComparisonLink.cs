using System;
using System.Reflection;

namespace ExpectedObjects.Chain.Links
{
    class InstanceOfTypeComparisonLink: IComparisonLink
    {
        public LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next)
        { 
            var expected = linkComparisonContext.Expected;
            var actual = linkComparisonContext.Actual;

            if (!linkComparisonContext.IgnoreTypeInformation && !expected.GetType().IsInstanceOfType(actual))
            {
                return LinkComparisonResult.False;
            }

            return next(linkComparisonContext);
        }
    }
}