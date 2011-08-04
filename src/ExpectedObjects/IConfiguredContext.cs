using System.Collections.Generic;
using System.Reflection;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public interface IConfiguredContext
    {
        IEnumerable<IComparisonStrategy> Strategies { get; }
        IWriter Writer { get; }
        bool IgnoreTypes { get; }
        BindingFlags GetFieldBindingFlags();
    }
}