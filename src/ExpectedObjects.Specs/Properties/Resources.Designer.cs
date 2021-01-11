﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExpectedObjects.Specs.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ExpectedObjects.Specs.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected object did not match the actual object.
        ///
        ///The following issues were found:
        ///
        ///1) ComplexType.IndexType.Item[4]:
        ///
        ///  Expected:
        ///    5
        ///
        ///  Actual:
        ///    6
        ///.
        /// </summary>
        internal static string ExceptionMessage_001 {
            get {
                return ResourceManager.GetString("ExceptionMessage_001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected object did not match the actual object.
        ///
        ///The following issues were found:
        ///
        ///1) &lt;Anonymous&gt;.Array[0]:
        ///
        ///  Expected:
        ///    &lt;Anonymous&gt;
        ///    { 
        ///        Parent = new &lt;Anonymous&gt;
        ///        { 
        ///            Array = new Object[] { ... }
        ///        },
        ///        Id = 1
        ///    }
        ///
        ///  Actual:
        ///    element was missing
        ///
        ///
        ///
        ///2) &lt;Anonymous&gt;.Array:
        ///
        ///  The following elements were unexpected:
        ///  
        ///  &lt;Anonymous&gt;
        ///  { 
        ///      Parent = new &lt;Anonymous&gt;
        ///      { 
        ///          Array = new Object[] { ... }
        ///      },
        ///   [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ExceptionMessage_002 {
            get {
                return ResourceManager.GetString("ExceptionMessage_002", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected object did not match the actual object.
        ///
        ///The following issues were found:
        ///
        ///1) TypeWithIEnumerable.Objects[0]:
        ///
        ///  Expected:
        ///    &quot;test2&quot;
        ///
        ///  Actual:
        ///    &quot;test1&quot;
        ///
        ///
        ///
        ///2) TypeWithIEnumerable.Objects[1]:
        ///
        ///  Expected:
        ///    &quot;test1&quot;
        ///
        ///  Actual:
        ///    &quot;test2&quot;
        ///.
        /// </summary>
        internal static string ExceptionMessage_003 {
            get {
                return ResourceManager.GetString("ExceptionMessage_003", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected object did not match the actual object.
        ///
        ///The following issues were found:
        ///
        ///1) TypeWithStringMembers.StringProperty:
        ///
        ///  Expected:
        ///    &quot;same&quot;
        ///
        ///  Actual:
        ///    &quot;different&quot;
        ///
        ///
        ///
        ///2) TypeWithStringMembers.StringField:
        ///
        ///  Expected:
        ///    &quot;test&quot;
        ///
        ///  Actual:
        ///    &quot;test2&quot;
        ///.
        /// </summary>
        internal static string ExceptionMessage_004 {
            get {
                return ResourceManager.GetString("ExceptionMessage_004", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected object did not match the actual object.
        ///
        ///The following issues were found:
        ///
        ///1) &lt;Anonymous&gt;.Array[0].Id:
        ///
        ///  Expected:
        ///    1
        ///
        ///  Actual:
        ///    2
        ///.
        /// </summary>
        internal static string ExceptionMessage_005 {
            get {
                return ResourceManager.GetString("ExceptionMessage_005", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected object did not match the actual object.
        ///
        ///The following issues were found:
        ///
        ///1) TypeWithIEnumerable.Objects[0]:
        ///
        ///  Expected:
        ///    ComplexType
        ///    { 
        ///        IntegerProperty = 0,
        ///        DecimalProperty = 1.1m
        ///    }
        ///
        ///  Actual:
        ///    element was missing
        ///
        ///
        ///
        ///2) TypeWithIEnumerable.Objects[1]:
        ///
        ///  Expected:
        ///    ComplexType
        ///    { 
        ///        IntegerProperty = 0,
        ///        DecimalProperty = 1.2m
        ///    }
        ///
        ///  Actual:
        ///    element was missing
        ///
        ///
        ///
        ///3) TypeWithIEnumerable.Objects:
        ///
        ///  The f [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ExceptionMessage_006 {
            get {
                return ResourceManager.GetString("ExceptionMessage_006", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected object did not match the actual object.
        ///
        ///The following issues were found:
        ///
        ///1) &lt;Anonymous&gt;.Level1.Level2.Level3:
        ///
        ///  Expected:
        ///    &quot;test1&quot;
        ///
        ///  Actual:
        ///    &quot;test2&quot;
        ///
        ///
        ///
        ///2) &lt;Anonymous&gt;.StringProperty:
        ///
        ///  Expected:
        ///    &quot;test1&quot;
        ///
        ///  Actual:
        ///    &quot;test2&quot;
        ///.
        /// </summary>
        internal static string ExceptionMessage_007 {
            get {
                return ResourceManager.GetString("ExceptionMessage_007", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected object did not match the actual object.
        ///
        ///The following issues were found:
        ///
        ///1) List&lt;TypeWithDecimal&gt;[0]:
        ///
        ///  Expected:
        ///    TypeWithDecimal
        ///    { 
        ///        DecimalProperty = 1.1m
        ///    }
        ///
        ///  Actual:
        ///    element was missing
        ///
        ///
        ///
        ///2) List&lt;TypeWithDecimal&gt;[1]:
        ///
        ///  Expected:
        ///    TypeWithDecimal
        ///    { 
        ///        DecimalProperty = 1.2m
        ///    }
        ///
        ///  Actual:
        ///    element was missing
        ///
        ///
        ///
        ///3) List&lt;TypeWithDecimal&gt;:
        ///
        ///  The following elements were unexpected:
        ///  
        ///  TypeWithDecimal
        ///  { 
        ///      [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ExceptionMessage_008 {
            get {
                return ResourceManager.GetString("ExceptionMessage_008", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected object did not match the actual object.
        ///
        ///The following issues were found:
        ///
        ///1) TypeWithString.StringProperty:
        ///
        ///  Expected:
        ///    &quot;test2&quot;
        ///
        ///  Actual:
        ///    &quot;test&quot;
        ///.
        /// </summary>
        internal static string ExceptionMessage_009 {
            get {
                return ResourceManager.GetString("ExceptionMessage_009", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected object did not match the actual object.
        ///
        ///The following issues were found:
        ///
        ///1) Response.IsInStock:
        ///
        ///  Expected:
        ///    False
        ///
        ///  Actual:
        ///    True
        ///
        ///
        ///
        ///2) Response.Quantity:
        ///
        ///  Expected:
        ///    4
        ///
        ///  Actual:
        ///    [null]
        ///.
        /// </summary>
        internal static string ExceptionMessage_010 {
            get {
                return ResourceManager.GetString("ExceptionMessage_010", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected object did not match the actual object.
        ///
        ///The following issues were found:
        ///
        ///1) TypeWithElementList.Elements[0]:
        ///
        ///  Expected:
        ///    Element
        ///    { 
        ///        Id = 1,
        ///        Data = &quot;value&quot;
        ///    }
        ///
        ///  Actual:
        ///    element was missing
        ///
        ///
        ///
        ///2) TypeWithElementList.Elements:
        ///
        ///  The following elements were unexpected:
        ///  
        ///  Element
        ///  { 
        ///      Id = 2,
        ///      Data = &quot;value&quot;
        ///  }
        ///.
        /// </summary>
        internal static string ExceptionMessage_011 {
            get {
                return ResourceManager.GetString("ExceptionMessage_011", resourceCulture);
            }
        }
    }
}
