using System;

namespace ExpectedObjects
{
    class AbsolutePathMemberContext : IMemberContext
    {
        readonly IMemberConfigurationContext _memberConfigurationContext;
        readonly Type _rootType;
        readonly string _memberPath;

        public AbsolutePathMemberContext(IMemberConfigurationContext memberConfigurationContext, Type rootType, string memberPath)
        {
            _memberConfigurationContext = memberConfigurationContext;
            _rootType = rootType;
            _memberPath = memberPath;
        }

        public void UsesComparison(IComparison comparison)
        {
            _memberConfigurationContext.ConfigureMember(new AbsolutePathMemberComparison(comparison, _rootType, _memberPath));
        }
    }
}