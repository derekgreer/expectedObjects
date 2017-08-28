using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
	public class when_comparing_nulls
	{
		static bool _result;

		Because of = () => _result = ((object) null).ToExpectedObject().Equals((object) null);

		It should_be_equal = () => _result.ShouldBeTrue();
	}

	public class when_compariong_object_with_null_property
	{
		static ExpectedObject _expected;
		static ComplexType _actual;
		static bool _result;

		Establish context = () =>
			{
				_expected = new
					{
						TypeWithString = new { StringProperty = "test" }
					}.ToExpectedObject();

				_actual = new ComplexType
					{
						TypeWithString = null
					};
			};

		Because of = () => _result = _expected.Matches(_actual);

		It should_not_be_equal = () => _result.ShouldBeFalse();
	}
}