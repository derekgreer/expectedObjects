using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
#if NET40
using System.Collections.Generic;

#endif

namespace ExpectedObjects
{
    static class TypeExtensions
    {
        public static bool IsAnonymousType(this Type type)
        {
            var hasCompilerGeneratedAttribute =
                type.GetTypeInfo().GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any();
            var nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            var isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;

            return isAnonymousType;
        }
#if NET40
        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }

        public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type, string methodName)
        {
            return type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(mi => mi.Name == methodName);
        }
#endif
    }
}