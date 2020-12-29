using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExpectedObjects.Strategies
{
    public class KeyValuePairComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(object expected, object actual)
        {
            var expectedTypeInfo = expected.GetType().GetTypeInfo();
            var actualTypeInfo = actual.GetType().GetTypeInfo();

            if (expectedTypeInfo.IsGenericType && actualTypeInfo.IsGenericType)
                if (expectedTypeInfo.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                    return true;

            return false;
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            var genericTypes = expected.GetType().GetGenericArguments();

            var getKey = GetMethodInfo("GetKey", genericTypes);
            var key1 = getKey.Invoke(this, new[] {expected});
            var key2 = getKey.Invoke(this, new[] {actual});

            var areEqual = comparisonContext.ReportEquality(key1, key2, "Key");


            var getValue = GetMethodInfo("GetValue", genericTypes);
            var value1 = getValue.Invoke(this, new[] {expected});
            var value2 = getValue.Invoke(this, new[] {actual});

            areEqual = comparisonContext.ReportEquality(value1, value2, "Value") && areEqual;

            return areEqual;
        }

        MethodInfo GetMethodInfo(string methodName, Type[] genericTypes)
        {
            var methodInfo = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            return methodInfo.MakeGenericMethod(genericTypes);
        }

        object GetKey<TKey, TValue>(KeyValuePair<TKey, TValue> keyValuePair)
        {
            return keyValuePair.Key;
        }

        object GetValue<TKey, TValue>(KeyValuePair<TKey, TValue> keyValuePair)
        {
            return keyValuePair.Value;
        }
    }
}