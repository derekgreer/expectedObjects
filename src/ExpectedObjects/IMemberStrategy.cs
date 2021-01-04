namespace ExpectedObjects
{
    public interface IMemberStrategy
    {
        bool ShouldApply(string memberPath);

        IComparison Comparison { get; }
    }
}