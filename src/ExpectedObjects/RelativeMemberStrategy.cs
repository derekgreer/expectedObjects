namespace ExpectedObjects
{
    class RelativeMemberStrategy : IMemberStrategy
    {
        readonly string _memberPath;

        public RelativeMemberStrategy(IComparison comparison, string memberPath)
        {
            _memberPath = memberPath;
            Comparison = comparison;
        }

        public IComparison Comparison { get; }

        public bool ShouldApply(string memberPath)
        {
            return memberPath.EndsWith(_memberPath);
        }
    }
}