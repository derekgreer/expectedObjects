using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class TypeMismatchSpecs
    {
        [Subject("Non-matching Types")]
        class when_comparing_mismatch_types
        {
            static ExpectedObject _expected;
            static SimpleType _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new
                {
                    StringProperty = 1
                }.ToExpectedObject();

                _actual = new SimpleType
                {
                    StringProperty = "1"
                };
            };

            Because of = () => _exception = Catch.Exception(() => _expected.Matches(_actual));

            It should_not_throw_an_exception = () => _exception.ShouldBeNull();
        }
    }
}