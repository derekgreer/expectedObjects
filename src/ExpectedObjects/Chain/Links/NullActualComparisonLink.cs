using System;

namespace ExpectedObjects.Chain.Links
{
    class NullActualComparisonLink: IComparisonLink
    {
        public LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next)
        { 
            var actual = linkComparisonContext.Actual;

            if (actual == null)
            {
                return LinkComparisonResult.False;
            }

            return next(linkComparisonContext);
        }
    }
}