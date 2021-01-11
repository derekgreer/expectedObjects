using System.Collections.Generic;
using System.Linq;
using ExpectedObjects.Comparisons;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class AnyComparisonSpecs
    {
        [Subject("Any Comparison")]
        class when_comparing_any_with_valid_matching_comparison
        {
            static ComplexType _actual;
            static ExpectedObject _expected;

            static bool _result;

            Establish context = () =>
            {
                _expected = new
                {
                    StringProperty = new Any<string>()
                }.ToExpectedObject();

                _actual = new ComplexType
                {
                    StringProperty = "test string"
                };
            };

            Because of = () => _result = _expected.Matches(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Any Comparison")]
        class when_comparing_any_with_invalid_comparison
        {
            static ComplexType _actual;
            static ExpectedObject _expected;

            static bool _result;

            Establish context = () =>
            {
                _expected = new
                {
                    StringProperty = new Any<int>()
                }.ToExpectedObject();

                _actual = new ComplexType
                {
                    StringProperty = "test string"
                };
            };

            Because of = () => _result = _expected.Matches(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }

        [Subject("Any Comparison")]
        class when_comparing_any_predicate_with_invalid_comparison
        {
            static TypeWithIEnumerable<ComplexType> _actual;
            static ExpectedObject _expected;

            static bool _result;

            Establish context = () =>
            {
                _expected = new
                {
                    Objects = Expect.Any<IEnumerable<ComplexType>>(list => list.All(i => i.IntegerProperty > 3))
                }.ToExpectedObject();

                _actual = new TypeWithIEnumerable<ComplexType>
                {
                    Objects = new List<ComplexType>(new[]
                    {
                        new ComplexType {IntegerProperty = 3}
                    })
                };
            };

            Because of = () => _result = _expected.Matches(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }
    }
}