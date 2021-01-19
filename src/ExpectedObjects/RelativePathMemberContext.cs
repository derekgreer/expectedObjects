namespace ExpectedObjects
{
    class RelativePathMemberContext : IMemberContext
    {
        readonly IMemberConfigurationContext _memberConfigurationContext;
        readonly string _memberPath;

        public RelativePathMemberContext(IMemberConfigurationContext memberConfigurationContext, string memberPath)
        {
            _memberConfigurationContext = memberConfigurationContext;
            _memberPath = memberPath;
        }

        public void UsesComparison(IComparison comparison)
        {
            _memberConfigurationContext.ConfigureMember(new RelativeMemberComparison(comparison, _memberPath));
        }
    }
}