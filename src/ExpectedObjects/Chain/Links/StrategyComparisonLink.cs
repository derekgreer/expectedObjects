using System;

namespace ExpectedObjects.Chain.Links
{
    class StrategyComparisonLink: IComparisonLink
    {
        public LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next)
        {
            var expected = linkComparisonContext.Expected;
            var actual = linkComparisonContext.Actual;

            foreach (var strategy in linkComparisonContext.Configuration.ComparisonStrategies)
                if (strategy.CanCompare(expected, actual))
                {
                    var areEqual = strategy.AreEqual(expected, actual, linkComparisonContext.ComparisonContext);

                    return new LinkComparisonResult {Result = areEqual};
                }


            return next(linkComparisonContext);
        }
    }
}