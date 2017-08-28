using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public class ConfigurationContext : IConfigurationContext, IConfiguration
    {
        MemberType _memberType;
        Stack<IComparisonStrategy> _strategies = new Stack<IComparisonStrategy>();

        public IEnumerable<IComparisonStrategy> Strategies => _strategies;

        public BindingFlags GetFieldBindingFlags()
        {
            BindingFlags flags = 0;

            if ((_memberType & MemberType.PublicFields) == MemberType.PublicFields)
                flags |= BindingFlags.Public | BindingFlags.Instance;

            return flags;
        }

        public void PushStrategy<T>() where T : IComparisonStrategy, new()
        {
            _strategies.Push(new T());
        }

        public void PushStrategy(IComparisonStrategy comparisonStrategy)
        {
            _strategies.Push(comparisonStrategy);
        }

        public void IncludeMemberTypes(MemberType memberType)
        {
            _memberType |= memberType;
        }

        public void ReplaceStrategy<TExistingStrategy, TNewStrategy>() where TExistingStrategy : IComparisonStrategy
            where TNewStrategy : IComparisonStrategy, new()
        {
            var list = _strategies.ToList();
            var existingIndex = list.FindIndex(s => typeof(TExistingStrategy) == s.GetType());
            list.RemoveAt(existingIndex);
            list.Insert(existingIndex, new TNewStrategy());
            list.Reverse();
            _strategies = new Stack<IComparisonStrategy>(list);
        }
    }
}