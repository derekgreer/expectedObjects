using System.Collections.Generic;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    public class ExpectedObjectBuilder
    {
        readonly IConfigurationContext _configurationContext = new ConfigurationContext();
        object _instance;

        public ExpectedObject Build()
        {
            return new ExpectedObject(_instance, (IConfiguration) _configurationContext);
        }

        public ExpectedObjectBuilder UsingStrategies(List<IComparisonStrategy> comparisonStrategies)
        {
            foreach (var strategy in comparisonStrategies)
            {
                _configurationContext.PushStrategy(strategy);
            }

            return this;
        }

        public ExpectedObjectBuilder UsingInstance(object instance)
        {
            _instance = instance;
            return this;
        }

        public ExpectedObjectBuilder UsingMemberTypes(MemberType memberTypeFlags)
        {
            _configurationContext.IncludeMemberTypes(memberTypeFlags);
            return this;
        }

        public ExpectedObjectBuilder PushStrategy(IComparisonStrategy comparisonStrategy)
        {
            _configurationContext.PushStrategy(comparisonStrategy);
            return this;
        }
    }
}