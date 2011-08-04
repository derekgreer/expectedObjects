using System;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
	[Subject(typeof (ExpectedObject))]
	public class when_comparing_types_which_hide_properties
	{
		static HidingType _expected;
		static HidingType _actual;
		static Exception _exception;

		Establish context = () =>
			{
				_expected = new HidingType {Value = 1};
				_actual = new HidingType {Value = 1};
			};

		Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().Equals(_actual));

		It should_not_throw_an_exception = () => _exception.ShouldBeNull();
	}

	abstract class BaseType
	{
		public virtual long Value { get; set; }
		public virtual int	OverriddenValue { get; set; }
		public virtual string InheritedValue { get; set; }
	}

	class HidingType : BaseType
	{
		public new int Value { get; set; }
		public override int OverriddenValue { get; set; }
		public virtual string AddedValue { get; set; }
	}
}