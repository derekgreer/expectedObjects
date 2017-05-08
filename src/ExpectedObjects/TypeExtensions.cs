using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExpectedObjects
{
#if  NET40
    internal static class TypeExtensions
    {

        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }

        public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type, string methodName)
        {
            return type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(mi => mi.Name == methodName);
        }
    }
#endif

}
