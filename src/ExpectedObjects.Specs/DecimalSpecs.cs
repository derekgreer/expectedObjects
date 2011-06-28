using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_comparing_equal_decimals_for_equality
    {
        static decimal _actual;
        static decimal _expected;
        static bool _result;

        Establish context = () =>
            {
                _expected = 10.222333m;
                _actual = 10.222333m;
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    public class when_comparing_unequal_decimals_for_equality
    {
        static decimal _actual;
        static decimal _expected;
        static bool _result;

        Establish context = () =>
        {
            _expected = 10.222333m;
            _actual = 10.222334m;
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}