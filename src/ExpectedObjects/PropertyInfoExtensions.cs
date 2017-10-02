using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExpectedObjects
{
    //public static class PropertyInfoExtensions
    //{
    //    public static IEnumerable<PropertyInfo> ExcludeHiddenProperties(this IEnumerable<PropertyInfo> infos, Type originType)
    //    {
    //        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
    //        var hiddenTypes = originType.GetProperties(flags).GroupBy(p => p.Name).Where(g => g.Count() > 1)
    //            .SelectMany(g => g.Where(t => t.DeclaringType != originType));
    //        return infos.Except(hiddenTypes);
    //    }
    //}

    public static class PropertyInfoExtensions
    {
        public static IEnumerable<PropertyInfo> GetVisibleProperties(this Type type, BindingFlags bindingFlags)
        {
            var properties = new List<PropertyInfo>();

            var declaredProperties = type.GetProperties(bindingFlags).Where(t => t.DeclaringType == type).ToList();

            properties.AddRange(declaredProperties);

            var baseType = type.GetTypeInfo().BaseType;
            if (baseType != null)
            {
                properties.AddRange(GetVisibleProperties(baseType, bindingFlags, declaredProperties));
            }

            return properties;
        }

        static IEnumerable<PropertyInfo> GetVisibleProperties(Type type, BindingFlags bindingFlags, IList<PropertyInfo> filterProperties)
        {
            var properties = new List<PropertyInfo>();

            var declaredVisibleProperties = type.GetProperties(bindingFlags).Where(t => t.DeclaringType == type &&
                                                        !filterProperties.Any(f => f.Name == t.Name && t.HasSameIndexParameters(f))).ToList();

            properties.AddRange(declaredVisibleProperties);

            var baseType = type.GetTypeInfo().BaseType;
            if (baseType != null)
            {
                properties.AddRange(GetVisibleProperties(baseType, bindingFlags, declaredVisibleProperties));
            }

            return properties;
        }

        public static bool HasSameIndexParameters(this PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
        {
            var param1 = propertyInfo1.GetIndexParameters().ToList();
            var param2 = propertyInfo2.GetIndexParameters().ToList();

            if (param1.Count != param2.Count)
                return false;

            for (var i = 0; i < param1.Count; i++)
                if (param1[i] != param2[i])
                    return false;

            return true;
        }
    }
}