using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_comparing_unequal_dictionaries_of_string_string_with_different_count
    {
        static IDictionary<string, string> _actual;
        static IDictionary<string, string> _expected;

        static bool _result;

        Establish context = () =>
            {
                _expected = new Dictionary<string, string> { { "key2", "value2" } };
                _actual = new Dictionary<string, string> {{"key1", "value1"}, {"key2", "value2"}};
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    public class when_comparing_unequal_dictionaries_of_string_string_with_same_count
    {
        static IDictionary<string, string> _actual;
        static IDictionary<string, string> _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new Dictionary<string, string> {{"key1", "value1"}};
                _expected = new Dictionary<string, string> {{"key2", "value2"}};
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    public class when_comparing_equal_dictionaries_of_string_string
    {
        static IDictionary<string, string> _actual;
        static IDictionary<string, string> _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new Dictionary<string, string> {{"key1", "value1"}};
                _expected = new Dictionary<string, string> {{"key1", "value1"}};
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    public class when_comparing_dictionaries_with_equal_int_keys_and_equal_but_different_object_values
    {
        static IDictionary<int, object> _actual;
        static IDictionary<int, object> _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new Dictionary<int, object> {{1, new object()}};
                _expected = new Dictionary<int, object> {{1, new object()}};
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    public class when_comparing_dictionaries_with_equal_key_of_int_and_value_of_same_object
    {
        static IDictionary<int, object> _actual;
        static IDictionary<int, object> _expected;

        static bool _result;

        Establish context = () =>
            {
                var someObject = new object();
                _actual = new Dictionary<int, object> {{1, someObject}};
                _expected = new Dictionary<int, object> {{1, someObject}};
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    public class when_comparing_equal_dictionaries_of_key_string_value_reference_type
    {
        static Dictionary<string, SimpleType> _actual;
        static Dictionary<string, SimpleType> _expected;
        static bool _result;

        Establish context = () =>
        {
            _expected = new Dictionary<string, SimpleType>
                            {
                                {"key1", new SimpleType {IntProperty = 1, StringProperty = "test"}}
                            };

            _actual = new Dictionary<string, SimpleType>
                            {
                                {"key1", new SimpleType {IntProperty = 1, StringProperty = "test"}}
                            };
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    public class when_comparing_equal_dictionaries_of_string_dictionary_string_object
    {
        static Dictionary<string, Dictionary<string, object>> _actual;
        static Dictionary<string, Dictionary<string, object>> _expected;
        static bool _result;

        Establish context = () =>
            {
                _expected = new Dictionary<string, Dictionary<string, object>>
                                {
                                    {"key", new Dictionary<string, object> {{"key2", "abc"}}},
                                };

                _actual = new Dictionary<string, Dictionary<string, object>>
                              {
                                  {"key", new Dictionary<string, object> {{"key2", "abc"}}},
                              };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    public class when_comparing_equal_key_value_pairs_of_string_reference_with_no_equals_overload
    {
        static KeyValuePair<string, SimpleType> _actual;
        static KeyValuePair<string, SimpleType> _expected;
        static bool _result;

        Establish context = () =>
            {
                _expected = new KeyValuePair<string, SimpleType>("test",
                                                                 new SimpleType
                                                                     {IntProperty = 1, StringProperty = "test"});

                _actual = new KeyValuePair<string, SimpleType>("test",
                                                                 new SimpleType { IntProperty = 1, StringProperty = "test" });

            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    public class when_comparing_unequal_key_value_pairs_with_equal_string_keys_and_unequal_reference_type_with_no_overload_values
    {
        static KeyValuePair<string, SimpleType> _actual;
        static KeyValuePair<string, SimpleType> _expected;
        static bool _result;

        Establish context = () =>
        {
            _expected = new KeyValuePair<string, SimpleType>("test",
                                                             new SimpleType { IntProperty = 1, StringProperty = "test1" });

            _actual = new KeyValuePair<string, SimpleType>("test",
                                                             new SimpleType { IntProperty = 2, StringProperty = "test2" });

        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}