using System;
using System.Collections.Generic;
using System.Reflection;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public interface IConfiguration
    {
        IEnumerable<IComparisonStrategy> ComparisonStrategies { get; }
        IEnumerable<IMemberComparison> MemberComparisons { get; }
        BindingFlags GetFieldBindingFlags();
        IComparisonStrategy GetTypeStrategy(Type type);
    }
}