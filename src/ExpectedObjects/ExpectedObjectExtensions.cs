using System;
using System.Linq.Expressions;
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

        public static void Like(this object actual, object expected)
        {
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        public static void IsExpectedToBeLike<T>(this T actual, T expected, params Expression<Func<T, object>>[] propertiesToIgnore)
        {
            expected.SetToDefault(propertiesToIgnore).ToExpectedObject().ShouldEqual(actual.SetToDefault(propertiesToIgnore));
        }

        public static void IsExpectedToBeSimilar(this object actual, object expected)
        {
            expected.ToExpectedObject().IgnoreTypes().ShouldEqual(actual);
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