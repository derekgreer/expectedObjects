using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{
	public class when_comparing_partial_anonymous_to_matching_type
	{
		static ComplexType _actual;
		static ExpectedObject _expected;

		static bool _result;

		Establish context = () =>
			{
				_expected = new {StringProperty = "test string"}.ToExpectedObject();

				_actual = new ComplexType
					{
						StringProperty = "test string",
						DecimalProperty = 10.10m,
						IndexType = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})
					};
			};

		Because of = () => _result = _expected.Matches(_actual);

		It should_be_equal = () => _result.ShouldBeTrue();
	}

	public class when_comparing_anonymous_to_unequal_type
	{
		static ComplexType _actual;
		static object _expected;

		static bool _result;

		Establish context = () =>
			{
				_expected = new
					{
						StringProperty = "test string",
						DecimalProperty = 10.10m,
						IndexType = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})
					}.ToExpectedObject();

				_actual = new ComplexType
					{
						StringProperty = "test string",
						DecimalProperty = 10.10m,
						IndexType = new IndexType<int>(new List<int> {1, 2, 3, 4, 6})
					};
			};

		Because of = () => _result = _expected.Equals(_actual);

		It should_not_be_equal = () => _result.ShouldBeFalse();
	}

	public class when_comparing_anonymous_to_equal_type
	{
		static ComplexType _actual;
		static object _expected;

		static bool _result;

		Establish context = () =>
			{
				_expected = new
					{
						StringProperty = "test string",
						DecimalProperty = 10.10m,
						IndexType = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})
					};

				_actual = new ComplexType
					{
						StringProperty = "test string",
						DecimalProperty = 10.10m,
						IndexType = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})
					};
			};

		Because of = () => _result = _expected.ToExpectedObject().Matches(_actual);

		It should_be_equal = () => _result.ShouldBeTrue();
	}
}