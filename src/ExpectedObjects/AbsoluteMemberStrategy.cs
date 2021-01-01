namespace ExpectedObjects
{
    public class AbsoluteMemberStrategy: IMemberStrategy
    {
        readonly string _memberPath;
        public IComparison Comparison { get; }

        public AbsoluteMemberStrategy(IComparison comparison, string memberPath)
        {
            _memberPath = memberPath;
            Comparison = comparison;
        }

        public bool ShouldApply(string absoluteMemberPath)
        {
            return absoluteMemberPath == _memberPath;
        }
    }
}