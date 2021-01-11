using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class ObjectShouldMatchExtensionSpecs
    {
        [Subject("ShouldMatch Extensions")]
        class when_comparing_different_types_with_equal_members_for_match_with_actual_as_subject
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
}