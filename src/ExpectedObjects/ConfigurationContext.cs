using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public class ConfigurationContext<TExpected> : IConfigurationContext<TExpected>, IConfiguration, IMemberConfigurationContext
    {
        MemberType _memberType;
        Stack<IComparisonStrategy> _strategies = new Stack<IComparisonStrategy>();
        readonly IList<IMemberStrategy> _memberStrategies = new List<IMemberStrategy>();

        public ConfigurationContext(TExpected @object)
        {
            Object = @object;
        }

        public IEnumerable<IComparisonStrategy> Strategies => _strategies;

        public IEnumerable<IMemberStrategy> MemberStrategies => _memberStrategies;

        public BindingFlags GetFieldBindingFlags()
        {
            BindingFlags flags = 0;

            if ((_memberType & MemberType.PublicFields) == MemberType.PublicFields)
                flags |= BindingFlags.Public | BindingFlags.Instance;

            return flags;
        }

        public object Object { get; }

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

            if (existingIndex == -1)
                throw new Exception($"The strategy {typeof(TExistingStrategy)} was not registered.");

            if (existingIndex >= 0)
            {
                list.RemoveAt(existingIndex);
                list.Insert(existingIndex, new TNewStrategy());
                list.Reverse();
                _strategies = new Stack<IComparisonStrategy>(list);
            }
        }

        public void ClearStrategies()
        {
            _strategies.Clear();
        }

        public IMemberContext Member<TMember>(Expression<Func<TExpected, TMember>> memberExpression)
        {
            return new MemberContext<TExpected, TMember>(this, Object.GetType(),  memberExpression);
        }

        void IMemberConfigurationContext.ConfigureMember(IMemberStrategy memberStrategy)
        {
            _memberStrategies.Add(memberStrategy);
        }
    }
}