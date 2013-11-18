using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_asserting_equality_for_objects_of_different_types_with_different_members
    {
        static TypeWithString _actual;
        static Exception _exception;
        static TypeWithDecimal _expected;

        Establish context = () =>
            {
                _expected = new TypeWithDecimal {DecimalProperty = 10.0m};
                _actual = new TypeWithString {StringProperty = "this is a test"};
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().IgnoreTypes().ShouldEqual(_actual));

        It should_thow_exception_with_missing_member_message = () => _exception.Message.ShouldEqual(string.Format(
            "For TypeWithString.DecimalProperty, expected [10.0] but member was missing.{0}", Environment.NewLine));
    }
}