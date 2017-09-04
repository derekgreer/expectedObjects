using System.Collections.Generic;
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

            foreach(var strategy in strategies)
                configurationContext.PushStrategy(strategy);

            return configurationContext;
        }

        /// <summary>
        /// Requires the order of <see cref="IEnumerable"/> members to be in the expected order.
        /// </summary>
        /// <param name="configurationContext"></param>
        /// <returns></returns>
        public static IConfigurationContext UseOrdinalComparison(this IConfigurationContext configurationContext)
        {
            configurationContext.ReplaceStrategy<EnumerableComparisonStrategy, OrdinalEnumerableComparisonStrategy>();
            return configurationContext;
        }
    }
}