using System;

namespace ExpectedObjects.Chain.Links
{
    class ReferenceEqualsComparisonLink : IComparisonLink
    {
        public LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next)
        {
            var expected = linkComparisonContext.Expected;
            var actual = linkComparisonContext.Actual;

            if (ReferenceEquals(expected, actual))
            {
                return new LinkComparisonResult() { Result = true};
            }

            return next(linkComparisonContext);
        }
    }
}