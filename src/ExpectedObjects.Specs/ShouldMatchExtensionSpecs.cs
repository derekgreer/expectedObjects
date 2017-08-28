using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_comparing_different_types_with_equal_members_for_match
    {
        static TypeWithString2 _actual;
        static Exception _exception;
        static ExpectedObject _expected;

        Establish context = () =>
        {
            _expected = new TypeWithString {StringProperty = "this is a test"}.ToExpectedObject();
            _actual = new TypeWithString2 {StringProperty = "this is a test"};
        };

        Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

        It should_not_throw_exception = () => _exception.ShouldBeNull();
    }
}