using System;

namespace ExpectedObjects.Chain.Links
{
    class MissingComparisonLink: IComparisonLink
    {
        public LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next)
        { 
            var actual = linkComparisonContext.Actual;

            if (actual is IMissing)
            {
                return LinkComparisonResult.False;
            }

            return next(linkComparisonContext);
        }
    }
}