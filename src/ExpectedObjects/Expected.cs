using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpectedObjects
{
    public static class Expected
    {
        public static ExpectedObject WithPropertiesFrom<TFrom, TTo>(TFrom from) 
            where TFrom : class
            where TTo : class
        {
            var to = Activator.CreateInstance<TTo>();
            ReflectionExtensions.CopyObject<TFrom, TTo>(from, ref to);
            return to.ToExpectedObject(true);

        }

        public static ExpectedObject WithSelectedProperties<TSource, TResult>(this TSource obj, params Expression<Func<TSource, dynamic>>[] items) where TSource : class
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

            if (result.GetType() != typeof(object))
            {
                foreach (var item in props)
                {
                    result.GetType().GetProperty(item.Key).SetValue(result, item.Value, null);
                }
            }
            else
            {
                var dyn = new ExpandoObject() as IDictionary<string, object>;
                foreach (var item in props)
                {
                    dyn.Add(item);
                }
            }

            return result;
        }

    }
}