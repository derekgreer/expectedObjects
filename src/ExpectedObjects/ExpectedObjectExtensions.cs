using System;
using ExpectedObjects.Reporting;

namespace ExpectedObjects
{
    public static class ExpectedObjectExtensions
    {
        public static ExpectedObject ToExpectedObject(this object expected)
        {
            return expected.ToExpectedObject(ctx => { ctx.UseAllStrategies(); });
        }

        public static ExpectedObject ToExpectedObject(this object expected,
            Action<IConfigurationContext> configurationAction)
        {
            var configurationContext = new ConfigurationContext();
            configurationContext.UseAllStrategies();
            configurationAction(configurationContext);
            return new ExpectedObject(expected, configurationContext);
        }

        public static bool Matches(this ExpectedObject expected, object actual)
        {
            return expected.Equals(actual, new NullWriter(), true);
        }

        public static bool DoesNotMatch(this ExpectedObject expected, object actual)
        {
            return !expected.Equals(actual, new NullWriter(), true);
        }
    }
}