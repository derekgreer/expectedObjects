using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
	public static class ExpectedObjectExtensions
	{
		public static ExpectedObject ToExpectedObject(this object expected)
		{
			return new ExpectedObject(expected).Configure(AddDefaultStrategies);
		}

        public static ExpectedObject ToExpectedObject(this object expected, bool checkUnmappedPropertiesOnActualMeetDefaultComparisons)
        {
            return new ExpectedObject(expected, checkUnmappedPropertiesOnActualMeetDefaultComparisons).IgnoreTypes().Configure(AutoPropertyStrategies);
        }

		public static ExpectedObject IgnoreTypes(this ExpectedObject expectedObject)
		{
			return expectedObject.Configure(ctx => ctx.IgnoreTypes());
		}

		static void AddDefaultStrategies(IConfigurationContext context)
		{
			context.PushStrategy<DefaultComparisonStrategy>();
			context.PushStrategy<KeyValuePairComparisonStrategy>();
			context.PushStrategy<ClassComparisonStrategy>();
			context.PushStrategy<EnumerableComparisonStrategy>();
			context.PushStrategy<EqualsOverrideComparisonStrategy>();
			context.PushStrategy<PrimitiveComparisonStrategy>();
			context.PushStrategy<ComparableComparisonStrategy>();
		}

        static void AutoPropertyStrategies(IConfigurationContext context)
        {
           
            context.PushStrategy<KeyValuePairComparisonStrategy>();
          //  context.PushStrategy<ClassComparisonStrategy>();
            context.PushStrategy<EnumerableComparisonStrategy>();
            context.PushStrategy<EqualsOverrideComparisonStrategy>();
            context.PushStrategy<PrimitiveComparisonStrategy>();
            context.PushStrategy<ComparableComparisonStrategy>();
      //      context.PushStrategy<DefaultComparisonStrategy>();
        }

	   
	}

}