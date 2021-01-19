using System;

namespace ExpectedObjects.Chain
{
    interface IComparisonLink
    {
        LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next);
    }
}