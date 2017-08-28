using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public interface IConfigurationContext
    {
        void PushStrategy<TStrategy>() where TStrategy : IComparisonStrategy, new();
        void PushStrategy(IComparisonStrategy comparisonStrategy);
        void IncludeMemberTypes(MemberType memberType);

        /// <summary>
        ///     Replace a previously registered strategy with a new strategy while preserving ordinality.
        /// </summary>
        /// <typeparam name="TExistingStrategy">The strategy to be removed</typeparam>
        /// <typeparam name="TNewStrategy">The strategy to be added</typeparam>
        void ReplaceStrategy<TExistingStrategy, TNewStrategy>()
            where TExistingStrategy : IComparisonStrategy
            where TNewStrategy : IComparisonStrategy, new();
    }
}