using System;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_asserting_equlity_for_equal_doubles_with_actual_as_subject
    {
        static Exception _exception;
        static ExpectedObject _expected;
        static double _actual;

        Establish context = () =>
            {
                _expected = 111.397720540215d.ToExpectedObject();
                _actual = 111.397720540215d;
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));

        It should_not_throw_an_exception = () => _exception.ShouldBeNull();
    }

    public class when_asserting_equlity_for_non_equal_doubles_with_actual_as_subject
    {
        static Exception _exception;
        static ExpectedObject _expected;
        static double _actual;

        Establish context = () =>
        {
            _expected = 111.397720540215d.ToExpectedObject();
            _actual = 111.397720540216d;
        };

        Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));

        It should_throw_an_exception_with_message =
            () =>
            _exception.Message.ShouldEqual(
                string.Format("For Double, expected [111.397720540215] but found [111.397720540216].{0}",
                              Environment.NewLine));
    }
}