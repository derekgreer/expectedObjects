using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExpectedObjects
{
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
                var baseProperties = GetVisibleProperties(baseType, bindingFlags, declaredProperties);

                foreach (var baseProperty in baseProperties)
                {
                    if (properties.All(p => p.Name != baseProperty.Name))
                    {
                        properties.Add(baseProperty);
                    }
                }
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