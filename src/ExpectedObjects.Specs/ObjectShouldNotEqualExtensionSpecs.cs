using System;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class ObjectShouldNotEqualExtensionSpecs
    {
        [Subject("UsesComparison Not Equal")]
        class when_asserting_inequlity_for_non_equal_doubles_with_actual_as_subject
        {
            static Exception _exception;
            static ExpectedObject _expected;
            static double _actual;

            Establish context = () =>
            {
                _expected = 111.397720540215d.ToExpectedObject();
                _actual = 111.397720540216d;
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldNotEqual(_actual));

            It should_throw_an_exception_with_message = () => _exception.ShouldBeNull();
        }
    }
}