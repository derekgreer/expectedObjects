using System;
using System.Linq.Expressions;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public interface IConfigurationContext
    {
        object Object { get; }

        /// <summary>
        /// Push a new strategy onto the top of the strategy chain.
        /// </summary>
        /// <typeparam name="TStrategy">strategy to add</typeparam>
        void PushStrategy<TStrategy>() where TStrategy : IComparisonStrategy, new();

        /// <summary>
        /// Push a new strategy onto the top of the strategy chain.
        /// </summary>
        /// <param name="comparisonStrategy">strategy to add</param>
        void PushStrategy(IComparisonStrategy comparisonStrategy);

        /// <summary>
        /// Specify which member types should be used in comparisons.
        /// </summary>
        /// <param name="memberType">member type flags</param>
        void IncludeMemberTypes(MemberType memberType);

        /// <summary>
        ///     Replace a previously registered strategy with a new strategy while preserving ordinality.
        /// </summary>
        /// <typeparam name="TExistingStrategy">the strategy to be removed</typeparam>
        /// <typeparam name="TNewStrategy">the strategy to be added</typeparam>
        void ReplaceStrategy<TExistingStrategy, TNewStrategy>()
            where TExistingStrategy : IComparisonStrategy
            where TNewStrategy : IComparisonStrategy, new();

        /// <summary>
        ///     Remove all strategies.
        /// </summary>
        void ClearStrategies();
    }

    public interface IConfigurationContext<T> : IConfigurationContext
    {
        /// <summary>
        /// Used for configuring members.
        /// </summary>
        /// <typeparam name="TMember">member type</typeparam>
        /// <param name="memberExpression">member expression</param>
        /// <returns></returns>
        IMemberContext Member<TMember>(Expression<Func<T, TMember>> memberExpression);
    }
}