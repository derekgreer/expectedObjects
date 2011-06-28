using System;

namespace ExpectedObjects
{
    public interface IComparisonStrategy
    {
        bool CanCompare(Type type);
        bool AreEqual(object expected, object actual, IComparisonContext comparisonContext);
    }
}