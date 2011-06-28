using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_asserting_equality_for_string_property_and_null
    {
        static Exception _exception;
        static TypeWithString _expected;

        Establish context = () =>
            {
                _expected = new TypeWithString
                                {
                                    StringProperty = "test"
                                };
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual((object) null));

        It should_thow_exception_with_null_message = () => _exception.Message.ShouldEqual(string.Format(
            "For [null], expected {0} but found [null].{1}", typeof(TypeWithString).FullName, Environment.NewLine));
    }
}