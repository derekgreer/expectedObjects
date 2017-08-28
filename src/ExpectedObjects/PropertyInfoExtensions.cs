using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExpectedObjects
{
    public static class PropertyInfoExtensions
    {
        public static IEnumerable<PropertyInfo> ExcludeHiddenProperties(this IEnumerable<PropertyInfo> infos, Type originType)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            List<string> newProperties =
                originType.GetProperties(flags).GroupBy(p => p.Name).Where(g => g.Count() > 1).Select(g => g.Key).ToList();

            if (!newProperties.Any())
                return infos;

            List<PropertyInfo> properties = infos.Where(p => !newProperties.Contains(p.Name)).ToList();

            foreach (string newProperty in newProperties)
            {
                Type declaringType = originType;
                PropertyInfo closestPropertyDeclaration = null;

                while (closestPropertyDeclaration == null)
                {
                    PropertyInfo pi = declaringType.GetProperties(flags)
                        .Where(p => p.Name == newProperty).SingleOrDefault(p => p.DeclaringType == declaringType);

                    if (pi != null)
                        closestPropertyDeclaration = pi;
                    else
                    {
                        declaringType = declaringType.GetTypeInfo().BaseType;
                    }
                }

                properties.Add(closestPropertyDeclaration);
            }

            return properties;
        }
    }
}