using System;

namespace ExpectedObjects
{
    class AbsolutePathMemberComparison : IMemberComparison
    {
        readonly Type _rootType;
        readonly string _memberPath;

        public AbsolutePathMemberComparison(IComparison comparison, Type rootType, string memberPath)
        {
            _rootType = rootType;
            _memberPath = memberPath;
            Comparison = comparison;
        }

        public IComparison Comparison { get; }

        public bool ShouldApply(string memberPath)
        {
            return memberPath == _memberPath;
        }
    }
}