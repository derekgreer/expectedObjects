using System;

namespace ExpectedObjects.Chain.Links
{
    class TrueLink: IComparisonLink
    {
        public LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next)
        {
            return LinkComparisonResult.True;
        }
    }
}