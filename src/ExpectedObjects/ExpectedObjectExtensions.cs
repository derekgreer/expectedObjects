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
            return new ExpectedObject(expected, checkUnmappedPropertiesOnActualMeetDefaultComparisons).IgnoreTypes().Configure(AddDefaultStrategies);
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


        public static ExpectedObject ToEoDto<TSource, TResult>(this TSource obj, params Expression<Func<TSource, dynamic>>[] items) where TSource : class
        {
            return ToDto<TSource, TResult>(obj, items).ToExpectedObject(true);
        }

        public static TResult ToDto<TSource, TResult>(this TSource obj, params Expression<Func<TSource, dynamic>>[] items) where TSource : class
        {
            var eo = new ExpandoObject();
            var props = eo as IDictionary<String, object>;

            foreach (var item in items)
            {
                var member = item.Body as MemberExpression;
                var unary = item.Body as UnaryExpression;
                var body = member ?? (unary != null ? unary.Operand as MemberExpression : null);

                if (member != null && body.Member is PropertyInfo)
                {
                    var property = body.Member as PropertyInfo;
                    props[property.Name] = obj.GetType()
                        .GetProperty(property.Name)
                        .GetValue(obj, null);
                }
                else
                {
                    var property = unary.Operand as MemberExpression;
                    if (property != null)
                    {
                        props[property.Member.Name] = obj.GetType()
                            .GetProperty(property.Member.Name)
                            .GetValue(obj, null);
                    }
                    else
                    {
                        var compiled = item.Compile();
                        var output = (KeyValuePair<string, object>)compiled.Invoke(obj);
                        props[output.Key] = obj.GetType()
                        .GetProperty(output.Value.ToString())
                        .GetValue(obj, null);
                    }
                }
            }

            TResult result = Activator.CreateInstance<TResult>();
            foreach (var item in props)
            {
                result.GetType().GetProperty(item.Key).SetValue(result, item.Value, null);
            }

            return result;
        }
	}
}