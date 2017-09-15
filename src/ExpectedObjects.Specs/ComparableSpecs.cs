using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_comparing_equal_comparables
    {
        static ComparableType _actual;
        static ComparableType _expected;

        static bool _result;

        Establish context = () =>
        {
            _actual = new ComparableType(true);
            _expected = new ComparableType(true);
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    public class when_comparing_unequal_comparables
    {
        static ComparableType _actual;
        static ComparableType _expected;

        static bool _result;

        Establish context = () =>
        {
            _actual = new ComparableType(true);
            _expected = new ComparableType(false);
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}