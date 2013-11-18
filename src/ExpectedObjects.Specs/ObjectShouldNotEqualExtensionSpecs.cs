using System;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_asserting_inequlity_for_equal_doubles_with_actual_as_subject
    {
        static Exception _exception;
        static ExpectedObject _expected;
        static double _actual;

        Establish context = () =>
        {
            _expected = 111.397720540215d.ToExpectedObject();
            _actual = 111.397720540215d;
        };

        Because of = () => _exception = Catch.Exception(() => _expected.ShouldNotEqual(_actual));

        It should_throw_an_exception_with_message =
           () =>
           _exception.Message.ShouldEqual(
               string.Format("For [111.397720540215], should not equal expected object but does.{0}",
                             Environment.NewLine));
    }

    public class when_asserting_inequlity_for_non_equal_doubles_with_actual_as_subject
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