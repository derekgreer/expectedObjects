using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public class ConfigurationContext<TExpected> : IConfigurationContext<TExpected>, IConfiguration, IMemberConfigurationContext
    {
        MemberType _memberType = MemberType.PublicFields;
        Stack<IComparisonStrategy> _strategies = new Stack<IComparisonStrategy>();
        readonly IList<IMemberComparison> _memberComparisons = new List<IMemberComparison>();
        IDictionary<Type, IComparisonStrategy> _typeStrategies = new Dictionary<Type, IComparisonStrategy>();

        public ConfigurationContext(TExpected @object)
        {
            Object = @object;
        }

        public IEnumerable<IComparisonStrategy> ComparisonStrategies => _strategies;

        public IEnumerable<IMemberComparison> MemberComparisons => _memberComparisons;

        public IComparisonStrategy GetTypeStrategy(Type type)
        {
            _typeStrategies.TryGetValue(type, out var strategy);
            return strategy;
        }

        public BindingFlags GetFieldBindingFlags()
        {
            BindingFlags flags = 0;

            if (_memberType.HasFlag(MemberType.PublicFields))
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

        public void RemoveStrategy<T>() where T: IComparisonStrategy
        {
            var list = _strategies.ToList();
            var existingIndex = list.FindIndex(s => typeof(T) == s.GetType());

            if (existingIndex >= 0)
            {
                list.RemoveAt(existingIndex);
                list.Reverse();
                _strategies = new Stack<IComparisonStrategy>(list);
            }
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

        public void MapStrategy<T>(IComparisonStrategy comparisonStrategy)
        {
            _typeStrategies.Add(typeof(T), comparisonStrategy);
        }

        public IMemberContext Member<TMember>(Expression<Func<TExpected, TMember>> memberExpression)
        {
            return new ExpressionMemberContext<TExpected, TMember>(this, Object.GetType(), memberExpression);
        }

        public IMemberContext Member(string memberPath)
        {
            return new AbsolutePathMemberContext(this, Object.GetType(), memberPath);
        }

        public IMemberContext RelativeMember(string memberPath)
        {
            return new RelativePathMemberContext(this, memberPath);
        }

        void IMemberConfigurationContext.ConfigureMember(IMemberComparison memberComparison)
        {
            _memberComparisons.Add(memberComparison);
        }
    }
}