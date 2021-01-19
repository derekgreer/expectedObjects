using System;

namespace ExpectedObjects.Chain.Links
{
    class NullExpectedComparisonLink: IComparisonLink
    {
        public LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next)
        { 
            var expected = linkComparisonContext.Expected;

            if (expected == null)
            {
                return new LinkComparisonResult() { Result = false};
            }

            return next(linkComparisonContext);
        }
    }
}