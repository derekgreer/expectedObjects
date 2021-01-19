using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public static class ConfigurationContextExtensions
    {
        /// <summary>
        /// Uses the default strategies for comparisons.
        /// </summary>
        /// <param name="configurationContext"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows for the use of custom strategies for performing comparisons.
        /// </summary>
        /// <param name="configurationContext"></param>
        /// <param name="strategies">strategies to use</param>
        /// <returns></returns>
        public static IConfigurationContext UseStrategies(this IConfigurationContext configurationContext, IEnumerable<IComparisonStrategy> strategies)
        {
            configurationContext.ClearStrategies();

            foreach (var strategy in strategies)
                configurationContext.PushStrategy(strategy);

            return configurationContext;
        }

        /// <summary>
        ///     Requires the order of any <see cref="IEnumerable" /> members to be in the expected order.
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
        ///     Ignores the specified absolute member in comparisons.
        /// </summary>
        /// <typeparam name="T">expected object type</typeparam>
        /// <param name="configurationContext"></param>
        /// <param name="memberPath">path to the ignored member</param>
        /// <returns></returns>
        public static IConfigurationContext<T> IgnoreAbsolutePath<T>(this IConfigurationContext<T> configurationContext, string memberPath)
        {
            configurationContext.Member(memberPath).UsesComparison(Expect.Ignored());
            return configurationContext;
        }

        /// <summary>
        ///     Ignores the specified relative member where path matches occur in comparisons.
        /// </summary>
        /// <typeparam name="T">expected object type</typeparam>
        /// <param name="configurationContext"></param>
        /// <param name="memberPath">relative path to ignored members</param>
        /// <returns></returns>
        public static IConfigurationContext<T> IgnoreRelativePath<T>(this IConfigurationContext<T> configurationContext, string memberPath)
        {
            configurationContext.RelativeMember(memberPath).UsesComparison(Expect.Ignored());
            return configurationContext;
        }

        /// <summary>
        ///     Ignores any overridden Equals() definition of equality.
        /// </summary>
        /// <param name="configurationContext"></param>
        /// <returns></returns>
        public static IConfigurationContext IgnoreEqualsOverride(this IConfigurationContext configurationContext)
        {
            configurationContext.RemoveStrategy<EqualsOverrideComparisonStrategy>();
            return configurationContext;
        }

        /// <summary>
        ///     Ignores any overridden Equals() definition of equality for specified type.
        /// </summary>
        /// <typeparam name="T">type to ignore Equals() override</typeparam>
        /// <param name="configurationContext"></param>
        /// <returns></returns>
        public static IConfigurationContext IgnoreEqualsOverrideForType<T>(this IConfigurationContext configurationContext)
        {
            configurationContext.MapStrategy<T>(new ClassComparisonStrategy());
            return configurationContext;
        }
    }
}