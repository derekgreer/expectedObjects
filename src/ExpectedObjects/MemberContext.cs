namespace ExpectedObjects
{
    public class MemberContext : IMemberContext
    {
        readonly IMemberConfigurationContext _memberConfigurationContext;
        readonly string _memberPath;

        public MemberContext(IMemberConfigurationContext memberConfigurationContext, string memberPath)
        {
            _memberConfigurationContext = memberConfigurationContext;
            _memberPath = memberPath;
        }

        public void UsesComparison(IComparison comparison)
        {
            _memberConfigurationContext.ConfigureMember(_memberPath, comparison);
        }
    }
}