namespace ExpectedObjects
{
    public interface IMemberComparison
    {
        bool ShouldApply(string memberPath);

        IComparison Comparison { get; }
    }
}