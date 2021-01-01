namespace ExpectedObjects
{
    public class RelativeMemberStrategy: IMemberStrategy
    {
        readonly string _relativeMemberPath;
        public IComparison Comparison { get; }

        public RelativeMemberStrategy(IComparison comparison, string relativeMemberPath)
        {
            _relativeMemberPath = relativeMemberPath;
            Comparison = comparison;
        }

        public bool ShouldApply(string absoluteMemberPath)
        {
            return absoluteMemberPath.EndsWith(_relativeMemberPath);
        }
    }
}