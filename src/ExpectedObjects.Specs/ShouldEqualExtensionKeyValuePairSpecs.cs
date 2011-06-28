using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_asserting_equality_for_key_value_pairs_with_unequal_int_keys_and_equal_string_values
    {
        static KeyValuePair<int, string> _actual;
        static Exception _exception;
        static KeyValuePair<int, string> _expected;

        Establish context = () =>
            {
                _expected = new KeyValuePair<int, string>(1, "test1");
                _actual = new KeyValuePair<int, string>(2, "test1");
            };

        Because of =
            () => _exception = Catch.Exception(() => _expected.ToExpectedObject().IgnoreTypes().ShouldEqual(_actual));

        It should_throw_exception_with_key_member_message = () => _exception.Message.ShouldEqual(string.Format(
            "For KeyValuePair`2.Key, expected [1] but found [2].{0}", Environment.NewLine));
    }

    public class when_asserting_equality_for_key_value_pairs_with_equal_int_keys_and_unequal_string_values
    {
        static KeyValuePair<int, string> _actual;
        static Exception _exception;
        static KeyValuePair<int, string> _expected;

        Establish context = () =>
            {
                _expected = new KeyValuePair<int, string>(1, "test1");
                _actual = new KeyValuePair<int, string>(1, "test2");
            };

        Because of =
            () => _exception = Catch.Exception(() => _expected.ToExpectedObject().IgnoreTypes().ShouldEqual(_actual));

        It should_throw_exception_with_key_member_message = () => _exception.Message.ShouldEqual(string.Format(
            "For KeyValuePair`2.Value, expected \"test1\" but found \"test2\".{0}", Environment.NewLine));
    }
}