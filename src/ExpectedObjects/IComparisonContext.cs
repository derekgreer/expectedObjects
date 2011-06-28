using System;
using System.Reflection;

namespace ExpectedObjects
{
    public interface IComparisonContext
    {
        bool AreEqual(object expected, object actual, string member);
        bool CompareProperties(object expected, object actual, Func<PropertyInfo, PropertyInfo, bool> comparison);
        bool CompareFields(object expected, object actual, Func<FieldInfo, FieldInfo, bool> comparison);
    }
}