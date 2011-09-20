using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
	public class when_comparing_different_types_with_same_member_for_equality
	{
		static TypeWithString2 _actual;
		static Exception _exception;
		static TypeWithString _expected;
		static ExpectedObject _expectedObject;

		Establish context = () =>
			{
				_expected = new TypeWithString
				            	{
				            		StringProperty = "this is a test"
				            	};

				_actual = new TypeWithString2
				          	{
				          		StringProperty = "this is a test"
				          	};

				_expectedObject = new ExpectedObject(_expected);
			};

		Because of = () => _exception = Catch.Exception(() => _expectedObject.ShouldEqual(_actual));

		It should_throw_exception_with_TypeWithString_message = () => _exception.Message.ShouldEqual(
			string.Format("For TypeWithString2, expected {0} but found {1}.{2}",
			              typeof (TypeWithString).FullName,
			              typeof (TypeWithString2).FullName,
			              Environment.NewLine));
	}

	public class when_comparing_different_types_with_unequal_members_for_match
	{
		static TypeWithString2 _actual;
		static Exception _exception;
		static ExpectedObject _expected;

		Establish context = () =>
			{
				_expected = new TypeWithString {StringProperty = "a"}.ToExpectedObject();
				_actual = new TypeWithString2 {StringProperty = "a"};
			};

		Because of = () => _exception = Catch.Exception(() => _expected.ShouldNotMatch(_actual));

		It should_throw_exception_with_StringProperty_message = () => _exception.Message.ShouldEqual(
			string.Format("For {0}, should not equal expected object but does.{1}",
            typeof(TypeWithString2).FullName, Environment.NewLine));
	}
}