namespace ExpectedObjects.Strategies
{
    public interface IComparisonStrategy
    {
        bool CanCompare(object expected, object actual);
        bool AreEqual(object expected, object actual, IComparisonContext comparisonContext);
    }
}