using System;
using System.Collections.Generic;
using ExpectedObjects.Comparisons;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{
    [Subject("Any Comparison")]
    public class when_comparing_any_with_matching_comparison
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
    public class when_comparing_any_with_nonmatching_comparison
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
}