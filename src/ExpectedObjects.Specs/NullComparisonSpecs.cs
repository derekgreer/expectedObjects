using System;
using System.Collections.Generic;
using ExpectedObjects.Comparisons;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{
	public class when_asserting_equality_for_equal_null_comparison
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
						StringProperty = new NullComparison()
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

	public class when_comparing_not_null_property_with_null_comparison
	{
		static ComplexType _actual;
		static ExpectedObject _expected;

		static bool _result;
		static Mock<IComparison> _comparisionSpy;

		Establish context = () =>
			{
				_expected = new
					{
						StringProperty = new NullComparison()
					}.ToExpectedObject().IgnoreTypes();

				_actual = new ComplexType
					{
						StringProperty = null,
						DecimalProperty = 10.10m,
						IndexType = new IndexType<int>(new List<int> { 1, 2, 3, 4, 5 })
					};
			};

		Because of = () => _result = _expected.Equals(_actual);

		It should_be_equal = () => _result.ShouldBeTrue();
	}

	public class when_asserting_equality_for_missing_null_comparison
	{
		static TypeWithDecimal _actual;
		static ExpectedObject _expected;

		static bool _result;
		static Mock<IComparison> _comparisionSpy;
		static Exception _exception;

		Establish context = () =>
		{
			_expected = new
			{
				StringProperty = new NullComparison()
			}.ToExpectedObject().IgnoreTypes();

			_actual = new TypeWithDecimal
			{
				DecimalProperty = 10.10m,
			};
		};

		Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));


		It should_throw_an_expection_with_message = () => _exception.Message.ShouldEqual(
			string.Format("For TypeWithDecimal.StringProperty, expected a null value but member was missing.{0}",
						  Environment.NewLine));
	}
}

