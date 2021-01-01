using System.Collections.Generic;
using System.Reflection;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public interface IConfiguration
    {
        IEnumerable<IComparisonStrategy> Strategies { get; }
        IEnumerable<IMemberStrategy> MemberStrategies { get; }
        BindingFlags GetFieldBindingFlags();
    }
}