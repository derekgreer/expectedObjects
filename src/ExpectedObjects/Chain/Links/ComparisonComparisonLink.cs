using System;

namespace ExpectedObjects.Chain.Links
{
    class ComparisonComparisonLink: IComparisonLink
    {
        public LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next)
        { 
            var expected = linkComparisonContext.Expected;
            var actual = linkComparisonContext.Actual;

            if (expected is IComparison comparison)
            {
                var areEqual = comparison.AreEqual(actual);

                return new LinkComparisonResult
                    {Result = areEqual, ExpectedResult = !areEqual ? comparison.GetExpectedResult() : string.Empty};
            }

            return next(linkComparisonContext);
        }
    }
}