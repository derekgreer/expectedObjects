using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class StrongTypedCustomComparisonSpecs
    {
        [Subject("Any Comparison")]
        public class when_comparing_any_with_valid_matching_comparison
        {
            static ComplexType _actual;
            static ExpectedObject _expected;

            static bool _result;

            Establish context = () =>
            {
                _expected = new ComplexType
                {
                    IntegerProperty = 42
                }.ToExpectedObject(ctx => ctx.Member(x => x.IntegerProperty).UsesComparison(Expect.Any<int>()));


                _actual = new ComplexType
                {
                    IntegerProperty = 4
                };
            };

            Because of = () => _result = _expected.Equals(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Any Comparison")]
        public class when_comparing_any_where_other_properties_do_not_match
        {
            static ComplexType _actual;
            static ExpectedObject _expected;

            static bool _result;

            Establish context = () =>
            {
                _expected = new ComplexType
                {
                    StringProperty = "one thing"
                }.ToExpectedObject(ctx => ctx.Member(x => x.IntegerProperty).UsesComparison(Expect.Any<int>()));

                _actual = new ComplexType
                {
                    IntegerProperty = 4,
                    StringProperty = "another thing"
                };
            };

            Because of = () => _result = _expected.Equals(_actual);

            It should_be_equal = () => _result.ShouldBeFalse();
        }

        [Subject("Any Comparison")]
        public class when_comparing_any_with_an_invalid_comparison
        {
            static ComplexType _actual;
            static ExpectedObject _expected;

            static bool _result;

            Establish context = () =>
            {
                _expected = new ComplexType
                {
                    StringProperty = "test string",
                    IntegerProperty = 42
                }.ToExpectedObject(ctx => ctx.Member(x => x.StringProperty).UsesComparison(Expect.Any<int>()));

                _actual = new ComplexType
                {
                    StringProperty = "test string",
                    IntegerProperty = 4
                };
            };

            Because of = () => _result = _expected.Equals(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }
    }
}