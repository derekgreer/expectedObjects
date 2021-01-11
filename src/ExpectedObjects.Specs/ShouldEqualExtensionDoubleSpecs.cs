using System;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class ShouldEqualExtensionDoubleSpecs
    {
        [Subject("ShouldEqual Extensions")]
        class when_asserting_equality_for_equal_doubles
        {
            static double _actual;
            static Exception _exception;
            static double _expected;

            Establish context = () =>
            {
                _expected = 111.397720540215d;
                _actual = 111.397720540215d;
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual(_actual));

            It should_not_throw_an_exception = () => _exception.ShouldBeNull();
        }
    }
}