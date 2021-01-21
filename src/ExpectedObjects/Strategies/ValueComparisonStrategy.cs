using System;
using System.Reflection;

namespace ExpectedObjects.Strategies
{
    /// <summary>
    /// Compares configured members by value
    /// </summary>
    /// <remarks>
    /// This strategy applies to comparisons where the expected or actual types contain members.
    /// </remarks>
    public class ValueComparisonStrategy : IComparisonStrategy
    {
        public bool CanCompare(object expected, object actual)
        {
            var expectedHasMembers = expected.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Length != 0 ||
                                     expected.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Length != 0;

            var actualHasMembers = expected.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Length != 0 ||
                                   expected.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Length != 0;

            var expectedTypeInfo = expected.GetType().GetTypeInfo();
            var actualTypeInfo = actual.GetType().GetTypeInfo();
            
            return (expectedHasMembers || actualHasMembers) &&
                expectedTypeInfo.IsClass && !expectedTypeInfo.IsArray &&
                actualTypeInfo.IsClass && !actualTypeInfo.IsArray;
        }

        public bool AreEqual(object expected, object actual, IComparisonContext comparisonContext)
        {
            const bool equal = true;
            var areEqual = comparisonContext.CompareProperties(expected, actual,
                (pi, actualPropertyInfo) =>
                    CompareProperty(pi, actualPropertyInfo, expected, actual,
                        comparisonContext, equal));

            areEqual = comparisonContext.CompareFields(expected, actual,
                           (fi, actualFieldInfo) =>
                               CompareField(fi, actualFieldInfo, expected, actual, comparisonContext)) && areEqual;

            return areEqual;
        }

        static bool CompareField(FieldInfo expectedFieldInfo, FieldInfo actualFieldInfo, object expected, object actual, IComparisonContext comparisonContext)
        {
            var value1 = expectedFieldInfo.GetValue(expected);

            if (actualFieldInfo == null)
                return comparisonContext
                    .ReportEquality(value1, Activator.CreateInstance(typeof(MissingMember<>)
                        .MakeGenericType(expectedFieldInfo.FieldType)), expectedFieldInfo.Name);

            var value2 = actualFieldInfo.GetValue(actual);
            return comparisonContext.ReportEquality(value1, value2, expectedFieldInfo.Name);
        }

        static bool CompareProperty(PropertyInfo expectedPropertyInfo, PropertyInfo actualPropertyInfo, object expected, object actual,
            IComparisonContext comparisonContext, bool areEqual)
        {
            var indexes = expectedPropertyInfo.GetIndexParameters();

            if (indexes.Length == 0)
                areEqual = CompareStandardProperty(expectedPropertyInfo, actualPropertyInfo, expected, actual, comparisonContext) &&
                           areEqual;
            else
                areEqual = CompareIndexedProperty(expectedPropertyInfo, expected, actual, indexes, comparisonContext) && areEqual;

            return areEqual;
        }

        static bool CompareIndexedProperty(PropertyInfo pi, object expected, object actual, ParameterInfo[] indexes, IComparisonContext comparisonContext)
        {
            var areEqual = true;

            foreach (var index in indexes)
                if (index.ParameterType == typeof(int))
                {
                    var expectedCountPropertyInfo = expected.GetType().GetProperty("Count");

                    var actualCountPropertyInfo = actual.GetType().GetProperty("Count");

                    if (expectedCountPropertyInfo != null)
                    {
                        var expectedCount = (int) expectedCountPropertyInfo.GetValue(expected, null);
                        var actualCount = (int) actualCountPropertyInfo.GetValue(actual, null);

                        if (expectedCount != actualCount)
                        {
                            areEqual = false;
                            break;
                        }

                        for (var i = 0; i < expectedCount; i++)
                        {
                            object[] indexValues = {i};
                            var value1 = pi.GetValue(expected, indexValues);
                            var value2 = pi.GetValue(actual, indexValues);

                            if (!comparisonContext.ReportEquality(value1, value2, pi.Name + "[" + i + "]"))
                                areEqual = false;
                        }
                    }
                }

            return areEqual;
        }

        static bool CompareStandardProperty(PropertyInfo pi1, PropertyInfo pi2, object expected, object actual,
            IComparisonContext comparisonContext)
        {
            var value1 = pi1.GetValue(expected, null);

            if (pi2 == null)
                return comparisonContext
                    .ReportEquality(value1, Activator.CreateInstance(typeof(MissingMember<>)
                        .MakeGenericType(pi1.PropertyType)), pi1.Name);

            var value2 = pi2.GetValue(actual, null);
            return comparisonContext.ReportEquality(value1, value2, pi1.Name);
        }
    }
}