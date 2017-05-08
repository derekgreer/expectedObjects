using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExpectedObjects.Strategies
{
    class KeyValuePairComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(Type type)
        {
            if (type.GetTypeInfo().IsGenericType)
            {
                if (type.GetGenericTypeDefinition().Equals(typeof (KeyValuePair<,>)))
                {
                    return true;
                }
            }

            return false;
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            bool areEqual = false;
            Type[] genericTypes = expected.GetType().GetGenericArguments();

            MethodInfo getKey = GetMethodInfo("GetKey", genericTypes);
            object key1 = getKey.Invoke(this, new[] {expected});
            object key2 = getKey.Invoke(this, new[] {actual});

            areEqual = comparisonContext.AreEqual(key1, key2, "Key");


            MethodInfo getValue = GetMethodInfo("GetValue", genericTypes);
            object value1 = getValue.Invoke(this, new[] {expected});
            object value2 = getValue.Invoke(this, new[] {actual});

            areEqual = comparisonContext.AreEqual(value1, value2, "Value") && areEqual;

            return areEqual;
        }

        MethodInfo GetMethodInfo(string methodName, Type[] genericTypes)
        {
            MethodInfo methodInfo = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
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