using System;

namespace ExpectedObjects.Chain.Links
{
    class TypeStrategyComparisonLink: IComparisonLink
    {
        public LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next)
        {
            var expected = linkComparisonContext.Expected;
            var actual = linkComparisonContext.Actual;

            if (expected != null)
            {
                var typeStrategy = linkComparisonContext.Configuration.GetTypeStrategy(expected.GetType());

                if (typeStrategy != null)
                {
                    return new LinkComparisonResult {Result = typeStrategy.AreEqual(expected, actual, linkComparisonContext.ComparisonContext)};
                }
            }

            return next(linkComparisonContext);
        }
    }
}