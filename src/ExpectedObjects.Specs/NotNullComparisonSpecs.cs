using System;
using System.Collections.Generic;
using ExpectedObjects.Comparisons;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{
	public class when_asserting_equality_for_not_equal_not_null_comparison
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
				StringProperty = new NotNullComparison()
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

	public class when_asserting_equality_for_missing_member_not_null_comparison
	{
		static object _actual;
		static ExpectedObject _expected;

		static bool _result;
		static Mock<IComparison> _comparisionSpy;
		static Exception _exception;

		Establish context = () =>
			{
				_expected = new
					{
						StringProperty = new NotNullComparison()
					}.ToExpectedObject().IgnoreTypes();

				_actual = new
					{
						DecimalProperty = 10.10m,
						IndexType = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})
					};
			};

		Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));


		It should_throw_an_expection_with_message = () => _exception.Message.ShouldEqual(
			string.Format("For <>f__AnonymousType5`2.StringProperty, expected a non-null value but member was missing.{0}",
			              Environment.NewLine));
	}

	public class when_asserting_equality_for_equal_not_null_comparison
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
				StringProperty = new NotNullComparison()
			}.ToExpectedObject().IgnoreTypes();

			_actual = new ComplexType
			{
				StringProperty = "a string",
				DecimalProperty = 10.10m,
				IndexType = new IndexType<int>(new List<int> { 1, 2, 3, 4, 5 })
			};
		};

		Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));


		It should_not_throw_an_expection = () => _exception.ShouldBeNull();
	}

	public class when_comparing_not_null_property_with_not_null_comparison
	{
		static ComplexType _actual;
		static ExpectedObject _expected;

		static bool _result;
		static Mock<IComparison> _comparisionSpy;

		Establish context = () =>
			{
				_expected = new
					{
						StringProperty = new NotNullComparison()
					}.ToExpectedObject().IgnoreTypes();

				_actual = new ComplexType
					{
						StringProperty = "test string",
						DecimalProperty = 10.10m,
						IndexType = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})
					};
			};

		Because of = () => _result = _expected.Equals(_actual);

		It should_be_equal = () => _result.ShouldBeTrue();
	}

	public class when_asserting_equality_for_missing_not_null_comparison
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
				ContainerProperty = new
				{
					StringProperty = new NotNullComparison()
				}
			}.ToExpectedObject().IgnoreTypes();

			_actual = new TypeWithDecimal
			{
				DecimalProperty = 10.10m,
			};
		};

		Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));


		It should_throw_an_expection_with_message = () => _exception.Message.ShouldEqual(
			string.Format("For TypeWithDecimal.ContainerProperty, expected <>f__AnonymousType0`1[ExpectedObjects.Comparisons.NotNullComparison]:[{{ StringProperty = ExpectedObjects.Comparisons.NotNullComparison }}] but member was missing.{0}",
						  Environment.NewLine));
	}
}