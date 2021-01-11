using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class ExpectedComparisonSpecs
    {
        [Subject("Comparisons")]
        class when_comparing_any_with_matching_comparison_using_it_isany
        {
            static ComplexType _actual;
            static ExpectedObject _expected;
            static bool _result;

            Establish context = () =>
            {
                _expected = new
                {
                    StringProperty = Expect.Any<string>()
                }.ToExpectedObject();

                _actual = new ComplexType
                {
                    StringProperty = "test string"
                };
            };

            Because of = () => _result = _expected.Matches(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Comparisons")]
        class when_comparing_any_with_non_matching_comparison_using_it_isany_matching_predicate
        {
            static ComplexType _actual;
            static ExpectedObject _expected;
            static bool _result;

            Establish context = () =>
            {
                _expected = new
                {
                    StringProperty = Expect.Any<string>(s => s != "test string")
                }.ToExpectedObject();

                _actual = new ComplexType
                {
                    StringProperty = "test string"
                };
            };

            Because of = () => _result = _expected.Matches(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }

        [Subject("Comparisons")]
        class when_comparing_any_with_matching_comparison_using_it_isany_matching_predicate
        {
            static ComplexType _actual;
            static ExpectedObject _expected;
            static bool _result;

            Establish context = () =>
            {
                _expected = new
                {
                    StringProperty = Expect.Any<string>(s => s.Length == 11)
                }.ToExpectedObject();

                _actual = new ComplexType
                {
                    StringProperty = "test string"
                };
            };

            Because of = () => _result = _expected.Matches(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Comparisons")]
        class when_comparing_any_with_matching_date_comparison_using_it_isany_matching_predicate
        {
            static object _actual;
            static ExpectedObject _expected;
            static bool _result;

            Establish context = () =>
            {
                _expected = new
                {
                    DateTimeProperty = Expect.Any<DateTime>(d => d > DateTime.MinValue)
                }.ToExpectedObject();

                _actual = new
                {
                    DateTimeProperty = DateTime.MaxValue
                };
            };

            Because of = () => _result = _expected.Matches(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }
    }
}