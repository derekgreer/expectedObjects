using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpectedObjects.Chain
{
    class StrategyChain
    {
        readonly IList<IComparisonLink> _links = new List<IComparisonLink>();

        public void Link(IComparisonLink typeStrategyComparisonLink)
        {
            _links.Add(typeStrategyComparisonLink);
        }

        public LinkComparisonResult Process(LinkComparisonContext linkComparisonContext)
        {
            return GetNextLink(new Queue<IComparisonLink>(_links))(linkComparisonContext);
        }

        Func<LinkComparisonContext, LinkComparisonResult> GetNextLink(Queue<IComparisonLink> queue)
        {
            if (!queue.Any())
                return c => null;

            return context =>queue.Dequeue().Compare(context, GetNextLink(queue));
        }
    }
}