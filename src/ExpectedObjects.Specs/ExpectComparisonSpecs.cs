using System;
using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{
	public class when_asserting_equality_for_equal_not_null_comparison_with_expect_not_null
	{
		static ComplexType _actual;
		static ExpectedObject _expected;

		static bool _result;
		static Mock<IComparison> _comparisionSpy;
		static Exception _exception;

		Establish context = () =>
			{
				_expected = new
					{
						StringProperty = Expect.NotNull()
					}.ToExpectedObject().IgnoreTypes();

				_actual = new ComplexType
					{
						StringProperty = null,
						DecimalProperty = 10.10m,
						IndexType = new IndexType<int>(new List<int> { 1, 2, 3, 4, 5 })
					};
			};

		Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));


		It should_throw_an_expection_with_message = () => _exception.Message.ShouldEqual(
			string.Format("For ComplexType.StringProperty, expected a non-null value but found [null].{0}",
			              Environment.NewLine));
	}

	public class when_asserting_equality_for_equal_null_comparison_with_expect_null
	{
		static ComplexType _actual;
		static ExpectedObject _expected;

		static bool _result;
		static Mock<IComparison> _comparisionSpy;
		static Exception _exception;

		Establish context = () =>
		{
			_expected = new
			{
				StringProperty = Expect.Null()
			}.ToExpectedObject().IgnoreTypes();

			_actual = new ComplexType
			{
				StringProperty = "string",
				DecimalProperty = 10.10m,
				IndexType = new IndexType<int>(new List<int> { 1, 2, 3, 4, 5 })
			};
		};

		Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));


		It should_throw_an_expection_with_message = () => _exception.Message.ShouldEqual(
			string.Format("For ComplexType.StringProperty, expected a null value but found \"string\".{0}",
						  Environment.NewLine));
	}

	public class when_comparing_any_with_matching_comparison_using_it_isany
	{
		static ComplexType _actual;
		static ExpectedObject _expected;

		static bool _result;
		static Mock<IComparison> _comparisionSpy;

		Establish context = () =>
			{
				_expected = new
					{
						StringProperty = Expect.Any<string>()
					}.ToExpectedObject().IgnoreTypes();

				_actual = new ComplexType
					{
						StringProperty = "test string"
					};
			};

		Because of = () => _result = _expected.Equals(_actual);

		It should_be_equal = () => _result.ShouldBeTrue();
	}
}