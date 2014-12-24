using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpectedObjects
{
    public static class ReflectionExtensions
    {

        public static void CopyObject<TFrom, TTo>(TFrom sourceObject, ref TTo destObject)
            where TFrom : class
            where TTo : class
        {
            if (sourceObject == null || destObject == null)
                return;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            //  Loop through the source properties
            foreach (PropertyInfo sourceProp in sourceType.GetProperties())
            {
                //  Get the matching property in the destination object
                PropertyInfo destProp = targetType.GetProperty(sourceProp.Name);
                //  If there is none, skip
                if (destProp == null)
                    continue;

                //  Set the value in the destination
                object value = sourceProp.GetValue(sourceObject, null);
                destProp.SetValue(destObject, value, null);
            }
        }

        public static KeyValuePair<string, object> Map<TFrom, TTo>(this TFrom instance, Expression<Func<TFrom, object>> from, Expression<Func<TTo, object>> to)
        {
            var toObj = Activator.CreateInstance<TTo>();
            return new KeyValuePair<string, object>(toObj.GetMemberName(to), instance.GetMemberName(from));
        }

        public static string GetMemberName<T>(
            this T instance,
            Expression<Func<T, object>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(
            Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(
            this T instance,
            Expression<Action<T>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(
            Expression<Action<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        private static string GetMemberName(
            Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression =
                    (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression =
                    (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException("Invalid expression");
        }

        private static string GetMemberName(
            UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression =
                    (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand)
                .Member.Name;
        }
    }
}
