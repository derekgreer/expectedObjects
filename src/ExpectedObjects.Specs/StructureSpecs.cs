using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class StructureSpecs
    {
        [Subject("Structures")]
        class when_comparing_unequal_structures
        {
            static StructureType _actual;
            static StructureType _expected;
            static bool _result;

            Establish context = () =>
            {
                _expected = new StructureType {IntegerValue = 0};
                _actual = new StructureType {IntegerValue = 1};
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }

        [Subject("Structures")]
        class when_comparing_equal_structures
        {
            static StructureType _actual;
            static StructureType _expected;
            static bool _result;

            Establish context = () =>
            {
                _expected = new StructureType {IntegerValue = 2};
                _actual = new StructureType {IntegerValue = 2};
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }
    }
}