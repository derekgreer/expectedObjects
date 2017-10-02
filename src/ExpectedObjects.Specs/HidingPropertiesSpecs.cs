using System;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class HidingTypeSpecs
    {
        public class when_comparing_types_which_hide_properties
        {
            static ExpectedObject _expected;
            static HidingType _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new HidingType {Value = 1, NonHiddenValue = 2}.ToExpectedObject();
                _actual = new HidingType {Value = 1, NonHiddenValue = 2};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.Equals(_actual));

            It should_be_equal = () => _expected.ShouldEqual(_actual);

            It should_not_throw_an_exception = () => _exception.ShouldBeNull();
        }

        public class when_comparing_unequal_types_which_hide_properties
        {
            static ExpectedObject _expected;
            static HidingType _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new HidingType {Value = 1, NonHiddenValue = 2}.ToExpectedObject();
                _actual = new HidingType {Value = 1, NonHiddenValue = 3};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.Equals(_actual));

            It should_not_be_equal = () => _expected.ShouldNotEqual(_actual);

            It should_not_throw_an_exception = () => _exception.ShouldBeNull();
        }

        public class when_comparing_unequal_types_which_hide_unequal_properties
        {
            static ExpectedObject _expected;
            static HidingType _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new HidingType {Value = 1}.ToExpectedObject();
                _actual = new HidingType {Value = 2};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.Equals(_actual));

            It should_not_be_equal = () => _expected.ShouldNotEqual(_actual);

            It should_not_throw_an_exception = () => _exception.ShouldBeNull();
        }
    }


    abstract class BaseType
    {
        public virtual long Value { get; set; }
        public virtual int OverriddenValue { get; set; }
        public virtual string InheritedValue { get; set; }
    }

    class HidingTypeBase : BaseType
    {
        public int NonHiddenValue { get; set; }
        public new int Value { get; set; }
        public override int OverriddenValue { get; set; }
        public virtual string AddedValue { get; set; }
    }

    class HidingType : HidingTypeBase
    {
    }
}