using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{

    public static class Expected
    {
        public static ExpectedObject WithSameProperties<TTo>(this object from)            
            where TTo : class
        {
            var to = Activator.CreateInstance<TTo>();
            ReflectionExtensions.CopyObject(from, ref to);
            return to.ToDto<TTo, TTo>(true).ToExpectedObject(true);
        }

        public static ExpectedObject WithSelectedProperties<TSource, TResult>(this TSource obj,
            params Expression<Func<TSource, dynamic>>[] items)
            where TSource : class
            where TResult : class
        {
            return ToDto<TSource, TResult>(obj, true, items).ToExpectedObject(true);
        }

        public static ExpectedObject ToDto<TSource, TResult>(this TSource obj, bool remainingPropertiesHaveDefaultComparisons,
            params Expression<Func<TSource, dynamic>>[] items)
            where TSource : class
            where TResult : class
        {
            var eo = new ExpandoObject();
            var props = eo as IDictionary<String, object>;

            foreach (var item in items)
            {
                var member = item.Body as MemberExpression;
                var unary = item.Body as UnaryExpression;
                MemberExpression body = member ?? (unary != null ? unary.Operand as MemberExpression : null);

                if (member != null && body.Member is PropertyInfo)
                {
                    var property = body.Member as PropertyInfo;
                    props[property.Name] = obj.GetType()
                        .GetProperty(property.Name)
                        .GetValue(obj, null);
                }
                else
                {
                    var property = unary.Operand as MemberExpression;
                    if (property != null)
                    {
                        props[property.Member.Name] = obj.GetType()
                            .GetProperty(property.Member.Name)
                            .GetValue(obj, null);
                    }
                    else
                    {
                        Func<TSource, dynamic> compiled = item.Compile();
                        var output = (KeyValuePair<string, object>)compiled.Invoke(obj);
                        props[output.Key] = obj.GetType()
                            .GetProperty(output.Value.ToString())
                            .GetValue(obj, null);
                    }
                }
            }

            if (remainingPropertiesHaveDefaultComparisons)
            {
                var propertyInfos = obj.GetType().GetProperties().Where(a => a.MemberType.Equals(MemberTypes.Property)).ToArray();

                foreach (var propertyInfo in propertyInfos)
                {
                    if (!props.ContainsKey(propertyInfo.Name))
                    {
                        var compare = GetComparison(propertyInfo);
                        props.Add(new KeyValuePair<string, object>(propertyInfo.Name, compare));
                    }
                }
            }

            var dyn = new Expando();
            foreach (var item in props)
            {
                dyn.Properties.Add(item.Key, item.Value);
            }
            Debug.Assert(dyn.Properties.Any());

            return dyn.ToExpectedObject(true);
        }


        private static IComparison GetComparison(PropertyInfo info)
        {
            Type infoType = info.PropertyType;

            Type nullableType = null;

            if (infoType.IsGenericType && infoType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                nullableType = ExtractTypeFromNullable(infoType);
            }

            if (!info.CanWrite) return new NotDefaultComparison<object>();
            if (infoType == typeof(DateTime) || infoType == typeof(DateTime?))
            {
                return new NotDefaultComparison<DateTime>();
            }

            else if (infoType == typeof(string))
            {
                return new NotDefaultComparison<string>();
            }

            else if ((infoType.Equals(typeof(long)) || infoType.Equals(typeof(double)) ||
                      infoType.Equals(typeof(int)) || infoType.Equals(typeof(long)) ||
                      infoType.Equals(typeof(int))) && !info.Name.ToLower().EndsWith("id") &&
                     !info.Name.ToLower().Equals("pk"))
            {
                return new NotDefaultComparison<int>();
            }

            else if ((infoType.Equals(typeof(long?)) || infoType.Equals(typeof(double?)) ||
                      infoType.Equals(typeof(int?)) || infoType.Equals(typeof(long?)) ||
                      infoType.Equals(typeof(int?))) && !info.Name.ToLower().EndsWith("id") &&
                     !info.Name.ToLower().Equals("pk"))
            {

                return new NotDefaultComparison<int>();
            }

            else if (infoType.Equals(typeof(decimal)))
            {
                return new NotDefaultComparison<decimal>();
            }
            else if (infoType.Equals(typeof(bool)))
            {
                return new NotDefaultComparison<bool>();
            }

            else if (infoType.Equals(typeof(Guid)))
            {
                return new NotDefaultComparison<Guid>();
            }

            else if (infoType.Equals(typeof(Enum)))
            {
                return new NotDefaultComparison<Enum>();
            }

            else if (((infoType.BaseType != null) && (infoType.BaseType.Equals(typeof(Enum)))))
            {

                return new NotDefaultComparison<Enum>();
            }
            else if (infoType.IsArray)
            {
                return new NotDefaultComparison<IEnumerable<object>>();
            }

            return new NotDefaultComparison<object>();
        }

        /// <summary>Indentify and extracting type from Nullable Type</summary>
        public static Type ExtractTypeFromNullable(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                PropertyInfo valueProp = type.GetProperty("Value");

                return valueProp.PropertyType;
            }

            return null;
        }

        public static ExpectedObject ToExpectedObject(this object expected, bool checkUnmappedPropertiesOnActualMeetDefaultComparisons)
        {
            return new ExpectedObject(expected).IgnoreTypes().Configure(GetConfigurationContext);
        }

        public static void GetConfigurationContext(IConfigurationContext context)
        {
            context.PushStrategy<EnumerableComparisonStrategy>();
            context.PushStrategy<EqualsOverrideComparisonStrategy>();
            context.PushStrategy<PrimitiveComparisonStrategy>();
            context.PushStrategy<ComparableComparisonStrategy>();
        }
    }

    public class NotDefaultComparison<T> : IComparison
    {
        public bool AreEqual(object o)
        {
            if (o is Guid)
                return ((Guid) o) != Guid.Empty;

            if (!(o is T))
                return false;
            return !o.Equals(default(T));
        }

        public object GetExpectedResult()
        {
            return "a non-default value";
        }
    }

    public class StringContainsComparison : IComparison
    {
        private readonly string[] _items;
        private readonly string _contains;
        private List<string> _missing;

        public StringContainsComparison(string contains)
        {
            _contains = contains;
        }

        public StringContainsComparison(params string[] items)
        {
            _items = items;
        }

        public bool AreEqual(object o)
        {
            var value = o as string;

            if (IsSingleStringSearch())
            {
                return value != null && value.Contains(_contains);
            }

            _missing = new List<string>();

            foreach (var item in _items)
            {
                if (value != null && !value.Contains(item))
                    _missing.Add(item);
            }

            return _missing == null || !_missing.Any();
        }

        public object GetExpectedResult()
        {
            if (IsSingleStringSearch())
            {
                return "a string containing " + _contains;
            }

            var sb = new StringBuilder();
            sb.AppendLine("missing strings");
            _missing.ForEach(s => sb.AppendLine(s));
            return sb.ToString();

        }

        private bool IsSingleStringSearch()
        {
            return !string.IsNullOrEmpty(_contains);
        }
    }

    /// <summary>
    /// Class that provides extensible properties and methods. This
    /// dynamic object stores 'extra' properties in a dictionary or
    /// checks the actual properties of the instance.
    /// 
    /// This means you can subclass this expando and retrieve either
    /// native properties or properties from values in the dictionary.
    /// 
    /// This type allows you three ways to access its properties:
    /// 
    /// Directly: any explicitly declared properties are accessible
    /// Dynamic: dynamic cast allows access to dictionary and native properties/methods
    /// Dictionary: Any of the extended properties are accessible via IDictionary interface
    /// </summary>
    [Serializable]
    public class Expando : DynamicObject, IDynamicMetaObjectProvider
    {
        /// <summary>
        /// Instance of object passed in
        /// </summary>
        object Instance;

        /// <summary>
        /// Cached type of the instance
        /// </summary>
        Type InstanceType;

        PropertyInfo[] InstancePropertyInfo
        {
            get
            {
                if (_InstancePropertyInfo == null && Instance != null)
                    _InstancePropertyInfo = Instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                return _InstancePropertyInfo;
            }
        }
        PropertyInfo[] _InstancePropertyInfo;


        /// <summary>
        /// String Dictionary that contains the extra dynamic values
        /// stored on this object/instance
        /// </summary>        
        /// <remarks>Using PropertyBag to support XML Serialization of the dictionary</remarks>

        public readonly SerializableDictionary<string, object> Properties = new SerializableDictionary<string, object>();

        /// <summary>
        /// This constructor just works off the internal dictionary and any 
        /// public properties of this object.
        /// 
        /// Note you can subclass Expando.
        /// </summary>
        public Expando()
        {
            Initialize(this);
        }

        /// <summary>
        /// Allows passing in an existing instance variable to 'extend'.        
        /// </summary>
        /// <remarks>
        /// You can pass in null here if you don't want to 
        /// check native properties and only check the Dictionary!
        /// </remarks>
        /// <param name="instance"></param>
        public Expando(object instance)
        {
            Initialize(instance);
        }


        protected virtual void Initialize(object instance)
        {
            Instance = instance;
            if (instance != null)
                InstanceType = instance.GetType();
        }


        /// <summary>
        /// Try to retrieve a member by name first from instance properties
        /// followed by the collection entries.
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            // first check the Properties collection for member
            if (Properties.Keys.Contains(binder.Name))
            {
                result = Properties[binder.Name];
                return true;
            }


            // Next check for Public properties via Reflection
            if (Instance != null)
            {
                try
                {
                    return GetProperty(Instance, binder.Name, out result);
                }
                catch { }
            }

            // failed to retrieve a property
            result = null;
            return false;
        }


        /// <summary>
        /// Property setter implementation tries to retrieve value from instance 
        /// first then into this object
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {

            // first check to see if there's a native property to set
            if (Instance != null)
            {
                try
                {
                    bool result = SetProperty(Instance, binder.Name, value);
                    if (result)
                        return true;
                }
                catch { }
            }

            // no match - set or add to dictionary
            Properties[binder.Name] = value;
            return true;
        }

        /// <summary>
        /// Dynamic invocation method. Currently allows only for Reflection based
        /// operation (no ability to add methods dynamically).
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (Instance != null)
            {
                try
                {
                    // check instance passed in for methods to invoke
                    if (InvokeMethod(Instance, binder.Name, args, out result))
                        return true;
                }
                catch { }
            }

            result = null;
            return false;
        }


        /// <summary>
        /// Reflection Helper method to retrieve a property
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected bool GetProperty(object instance, string name, out object result)
        {
            if (instance == null)
                instance = this;

            var miArray = InstanceType.GetMember(name, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance);
            if (miArray != null && miArray.Length > 0)
            {
                var mi = miArray[0];
                if (mi.MemberType == MemberTypes.Property)
                {
                    result = ((PropertyInfo)mi).GetValue(instance, null);
                    return true;
                }
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Reflection helper method to set a property value
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool SetProperty(object instance, string name, object value)
        {
            if (instance == null)
                instance = this;

            var miArray = InstanceType.GetMember(name, BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);
            if (miArray != null && miArray.Length > 0)
            {
                var mi = miArray[0];
                if (mi.MemberType == MemberTypes.Property)
                {
                    ((PropertyInfo)mi).SetValue(Instance, value, null);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Reflection helper method to invoke a method
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected bool InvokeMethod(object instance, string name, object[] args, out object result)
        {
            if (instance == null)
                instance = this;

            // Look at the instanceType
            var miArray = InstanceType.GetMember(name,
                                    BindingFlags.InvokeMethod |
                                    BindingFlags.Public | BindingFlags.Instance);

            if (miArray != null && miArray.Length > 0)
            {
                var mi = miArray[0] as MethodInfo;
                result = mi.Invoke(Instance, args);
                return true;
            }

            result = null;
            return false;
        }



        /// <summary>
        /// Convenience method that provides a string Indexer 
        /// to the Properties collection AND the strongly typed
        /// properties of the object by name.
        /// 
        /// // dynamic
        /// exp["Address"] = "112 nowhere lane"; 
        /// // strong
        /// var name = exp["StronglyTypedProperty"] as string; 
        /// </summary>
        /// <remarks>
        /// The getter checks the Properties dictionary first
        /// then looks in PropertyInfo for properties.
        /// The setter checks the instance properties before
        /// checking the Properties dictionary.
        /// </remarks>
        /// <param name="key"></param>
        /// 
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                try
                {
                    // try to get from properties collection first
                    return Properties[key];
                }
                catch (KeyNotFoundException)
                {
                    // try reflection on instanceType
                    object result = null;
                    if (GetProperty(Instance, key, out result))
                        return result;

                    // nope doesn't exist
                    throw;
                }
            }
            set
            {
                if (Properties.ContainsKey(key))
                {
                    Properties[key] = value;
                    return;
                }

                // check instance for existance of type first
                var miArray = InstanceType.GetMember(key, BindingFlags.Public | BindingFlags.GetProperty);
                if (miArray != null && miArray.Length > 0)
                    SetProperty(Instance, key, value);
                else
                    Properties[key] = value;
            }
        }


        public IEnumerable<KeyValuePair<string, object>> GetProperties(bool includeInstanceProperties = false)
        {
            if (includeInstanceProperties && Instance != null)
            {
                foreach (var prop in this.InstancePropertyInfo)
                    yield return new KeyValuePair<string, object>(prop.Name, prop.GetValue(Instance, null));
            }

            foreach (var key in this.Properties.Keys)
                yield return new KeyValuePair<string, object>(key, this.Properties[key]);

        }


        public bool Contains(KeyValuePair<string, object> item, bool includeInstanceProperties = false)
        {
            bool res = Properties.ContainsKey(item.Key);
            if (res)
                return true;

            if (includeInstanceProperties && Instance != null)
            {
                foreach (var prop in this.InstancePropertyInfo)
                {
                    if (prop.Name == item.Key)
                        return true;
                }
            }

            return false;
        }


        public override bool Equals(object obj)
        {
            Type type = obj.GetType();

            foreach (PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (pi.CanWrite)
                {
                    if (!this.Properties.ContainsKey(pi.Name))
                    {
                        throw new MissingMemberException("  " + pi.Name + " missing");
                    }

                    object selfValue = this[pi.Name];
                    object toValue = obj.GetType().GetProperty(pi.Name).GetValue(obj, null);

                    if (selfValue is IComparison)
                    {
                        var comp = selfValue as IComparison;
                        return comp.AreEqual(toValue);
                    }
                    else
                    {
                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))) return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Properties.GetHashCode();
                hashCode = (hashCode * 397) ^ InstancePropertyInfo.GetHashCode();
                return hashCode;
            }
        }
    }


    [XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue>
        : Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
        #endregion
    }

}