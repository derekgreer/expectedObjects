using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public static class ConfigurationContextExtensions
    {
        internal static IConfigurationContext UseAllStrategies(this IConfigurationContext configurationContext)
        {
            configurationContext.PushStrategy<DefaultComparisonStrategy>();
            configurationContext.PushStrategy<KeyValuePairComparisonStrategy>();
            configurationContext.PushStrategy<ClassComparisonStrategy>();
            configurationContext.PushStrategy<EnumerableComparisonStrategy>();
            configurationContext.PushStrategy<EqualsOverrideComparisonStrategy>();
            configurationContext.PushStrategy<PrimitiveComparisonStrategy>();
            configurationContext.PushStrategy<ComparableComparisonStrategy>();
            return configurationContext;
        }

        public static IConfigurationContext UseStrategies(this IConfigurationContext configurationContext, IEnumerable<IComparisonStrategy> strategies)
        {
            configurationContext.ClearStrategies();

            foreach (var strategy in strategies)
                configurationContext.PushStrategy(strategy);

            return configurationContext;
        }

        /// <summary>
        ///     Requires the order of <see cref="IEnumerable" /> members to be in the expected order.
        /// </summary>
        /// <param name="configurationContext"></param>
        /// <returns></returns>
        public static IConfigurationContext UseOrdinalComparison(this IConfigurationContext configurationContext)
        {
            configurationContext.ReplaceStrategy<EnumerableComparisonStrategy, OrdinalEnumerableComparisonStrategy>();
            return configurationContext;
        }

        /// <summary>
        ///     Ignores the specified member in comparisons.
        /// </summary>
        /// <typeparam name="T">expected object type</typeparam>
        /// <typeparam name="TMember">member type</typeparam>
        /// <param name="configurationContext"></param>
        /// <param name="memberExpression">member expression</param>
        /// <returns></returns>
        public static IConfigurationContext<T> Ignore<T, TMember>(this IConfigurationContext<T> configurationContext,
            Expression<Func<T, TMember>> memberExpression)
        {
            configurationContext.Member(memberExpression).UsesComparison(Expect.Ignored());
            return configurationContext;
        }

        /// <summary>
        ///     Ignores the specified member in comparisons.
        /// </summary>
        /// <param name="memberPath">The path to the ignored member</param>
        /// <returns></returns>
        public static IConfigurationContext Ignore(this IConfigurationContext configurationContext, string memberPath)
        {
            ((IMemberConfigurationContext)configurationContext).ConfigureMember(new RelativeMemberStrategy(Expect.Ignored(), memberPath));
            return configurationContext;
        }
    }
}