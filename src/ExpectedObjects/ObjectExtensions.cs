using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ExpectedObjects.Chain.Links;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    static class ObjectExtensions
    {
        internal static string ToUsefulString(this object obj, bool verbose = false)
        {
            if (obj is IExpectedDescription)
                return obj.ToString();

            string str;
            if (obj == null) return "[null]";

            var description = obj as Description;
            if (description != null)
                if (description.Value != null)
                    return $"{description.Label} {ToUsefulString(description.Value, verbose)}";
                else
                    return $"{description.Label}";

            if (obj.GetType() == typeof(string))
            {
                str = (string) obj;

                if (string.IsNullOrWhiteSpace(str))
                    return "String.Empty";

                return "\"" + str.Replace("\n", "\\n") + "\"";
            }

            if (obj.GetType().GetTypeInfo().IsValueType) return obj.ToObjectString();

            if (obj is IEnumerable)
            {
                var enumerable = ((IEnumerable) obj).Cast<object>();

                return $"{obj.GetType().ToUsefulTypeName()}:{Environment.NewLine}{enumerable.EachToUsefulString(verbose)}";
            }

            str = verbose ? obj.ToObjectString() : obj.ToString();

            if (str == null || str.Trim() == "")
                return $"{obj.GetType()}:[]";

            if (!verbose)
            {
                if (str.Contains(Environment.NewLine))
                    return $@"{obj.GetType().ToUsefulTypeName()}:[{str.Tab()}]";

                if (obj.GetType().ToString() == str)
                    return obj.GetType().ToUsefulTypeName();

                str = $"{obj.GetType().ToUsefulTypeName()}:[{str}]";
            }

            return str;
        }

        static string EachToUsefulString<T>(this IEnumerable<T> enumerable, bool verbose = false)
        {
            var sb = new StringBuilder();
            sb.AppendLine("{");
            sb.Append(string.Join($",{Environment.NewLine}",
                enumerable.Select(x => ToUsefulString(x, verbose).Tab()).Take(10).ToArray()));
            if (enumerable.Count() > 10)
                if (enumerable.Count() > 11)
                    sb.AppendLine($",{Environment.NewLine}  ...({enumerable.Count() - 10} more elements)");
                else
                    sb.AppendLine($",{Environment.NewLine}" + enumerable.Last().ToUsefulString(verbose).Tab());
            else sb.AppendLine();
            sb.AppendLine("}");

            return sb.ToString();
        }

        static string Tab(this string str)
        {
            if (string.IsNullOrEmpty(str)) return "";

            var split = str.Split(new[] {$"{Environment.NewLine}"}, StringSplitOptions.None);
            var sb = new StringBuilder();

            sb.Append("  " + split[0]);
            foreach (var part in split.Skip(1))
            {
                sb.AppendLine();
                sb.Append("  " + part);
            }

            return sb.ToString();
        }
    }

    static class ObjectStringExtensions
    {
        static int _indentLevel;

        public static string ToObjectString(this object o)
        {
            var s = o as string;

            if (s != null)
                return $"\"{s}\"";

            return GetCSharpString(o);
        }

        public static string ToUsefulTypeName(this Type type)
        {
            if (type.IsAnonymousType())
            {
                return "<Anonymous>";
            }

            if (type.GetTypeInfo().IsGenericType)
            {
                var arg = type.GetGenericArguments().First().ToUsefulTypeName();
                return type.Name.Replace("`1", string.Format("<{0}>", arg));
            }

            return type.Name;
        }

        static string Prefix()
        {
            return new string(' ', _indentLevel * 2);
        }

        static StringBuilder CreateObject(this object o, Stack<object> visited = null)
        {
            var builder = new StringBuilder();

            if (visited == null)
                visited = new Stack<object>();

            if (visited.Contains(o))
            {
                builder.Append("... ");
                return builder;
            }

            visited.Push(o);

            if (_indentLevel > 0) builder.Append("new ");
            builder.Append($"{o.ToUsefulClassName()}{Environment.NewLine}{Prefix()}{{ ");
            _indentLevel++;

            _indentLevel++;

            var l = new List<string>();
            foreach (var fieldInfo in o.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = fieldInfo.GetValue(o);
                if (value != null)
                    l.Add($"{Prefix()}{fieldInfo.Name} = {value.GetCSharpString(visited)}");
            }

            foreach (var property in o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = property.GetValue(o, null);
                if (value != null)
                    l.Add($"{Prefix()}{property.Name} = {value.GetCSharpString(visited)}");
            }

            if (l.Any())
            {
                builder.Append(Environment.NewLine);
                builder.Append(string.Join($",{Environment.NewLine}", l));
            }
            _indentLevel -= 2;
            builder.Append($"{Environment.NewLine}{Prefix()}}}");

            visited.Pop();
            return builder;
        }

        static string ToUsefulClassName(this object o)
        {
            var type = o.GetType();
            return type.ToUsefulTypeName();
        }

        static string GetCSharpString(this object o, Stack<object> visited = null)
        {
            if (o == null) return "[null]";

            if (o is string)
                return $"\"{o}\"";
            if (o is decimal)
                return $"{o}m";
            if (IsNumericType(o))
                return $"{o}";
            if (o is Boolean)
                return $"{o}";
            if (o is DateTime)
                return $"DateTime.Parse(\"{o}\")";
            if (o is DateTimeOffset)
                return $"DateTimeOffset.Parse(\"{o}\")";
            if (o is IEnumerable)
                return $"new {o.ToUsefulClassName()} {{ {((IEnumerable) o).GetItems(visited)}}}";
            if (IsExpression(o))
                return o.ToString();

            return $"{o.CreateObject(visited)}";
        }
        
        public static bool IsExpression(object instance)
        {
            return instance.GetType().GetTypeInfo().IsSubclassOf(typeof(Expression));
        }

        static string GetItems(this IEnumerable items, Stack<object> visited = null)
        {
            var itemStrings = string.Join(",", items.Cast<object>().Select(i => i.GetCSharpString(visited)));
            return itemStrings;
        }

        static bool IsNumericType(this object o)
        {
            switch (Convert.GetTypeCode(o))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}