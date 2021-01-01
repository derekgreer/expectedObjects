namespace ExpectedObjects
{
    public interface IMemberStrategy
    {
        bool ShouldApply(string absoluteMemberPath);

        IComparison Comparison { get; }
    }
}