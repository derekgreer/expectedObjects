namespace ExpectedObjects
{
    class RelativeMemberComparison : IMemberComparison
    {
        readonly string _memberPath;

        public RelativeMemberComparison(IComparison comparison, string memberPath)
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