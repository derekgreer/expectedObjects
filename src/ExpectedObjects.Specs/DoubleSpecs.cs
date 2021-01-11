using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class DoubleSpecs
    {
        [Subject("Doubles")]
        class when_comparing_equal_doubles_for_equality
        {
            static double _actual;
            static double _expected;
            static bool _result;

            Establish context = () =>
            {
                _expected = 111.397720540215d;
                _actual = 111.397720540215d;
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Doubles")]
        class when_comparing_unequal_doubles_for_equality
        {
            static double _actual;
            static double _expected;
            static bool _result;

            Establish context = () =>
            {
                _expected = 111.397720540215d;
                _actual = 111.397720540216d;
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }
    }
}