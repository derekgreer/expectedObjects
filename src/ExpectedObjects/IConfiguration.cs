using System.Collections.Generic;
using System.Reflection;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public interface IConfiguration
    {
        IEnumerable<IComparisonStrategy> Strategies { get; }
        IDictionary<string, IComparison> MemberStrategies { get; }
        BindingFlags GetFieldBindingFlags();
    }
}