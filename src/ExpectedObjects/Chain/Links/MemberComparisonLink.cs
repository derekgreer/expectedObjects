using System;
using System.Linq;

namespace ExpectedObjects.Chain.Links
{
    class MemberComparisonLink : IComparisonLink
    {
        public LinkComparisonResult Compare(LinkComparisonContext linkComparisonContext, Func<LinkComparisonContext, LinkComparisonResult> next)
        {
            var expected = linkComparisonContext.Expected;
            var actual = linkComparisonContext.Actual;

            if (expected != null)
            {
                var memberComparison = GetMemberComparison(linkComparisonContext.MemberPath, linkComparisonContext.Configuration);

                if (memberComparison != null)
                {
                    var areEqual = memberComparison.AreEqual(actual);

                    return new LinkComparisonResult
                        {Result = areEqual, ExpectedResult = !areEqual ? memberComparison.GetExpectedResult() : string.Empty};
                }
            }

            return next(linkComparisonContext);
        }
        
        IComparison GetMemberComparison(string path, IConfiguration configuration)
        {
            var strategy = configuration.MemberComparisons.LastOrDefault(s => s.ShouldApply(path));

            return strategy?.Comparison;
        }
    }
}