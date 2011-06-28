using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_comparing_unequal_enums
    {
        static EnumType _actual;
        static EnumType _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = EnumType.Undefined;
                _expected = EnumType.Value1;
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    public class when_comparing_equal_enums
    {
        static EnumType _actual;
        static EnumType _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = EnumType.Undefined;
                _expected = EnumType.Undefined;
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}