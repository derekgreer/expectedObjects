using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
	public static class ExpectedObjectExtensions
	{
		public static ExpectedObject ToExpectedObject(this object expected)
		{
			return new ExpectedObject(expected).Configure(AddDefaultStrategies);
		}

		public static ExpectedObject WithDefaultStrategies(this ExpectedObject expectedObject)
		{
			return expectedObject.Configure(AddDefaultStrategies);
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
	}
}