using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;
using ExpectedObjects.Specs.Extensions;
using ExpectedObjects.Specs.Properties;

namespace ExpectedObjects.Specs
{
    class NullSpecs
    {
        class when_comparing_equal_types_with_missing_nullable_members
        {
            static ExpectedObject _expected;
            static TypeWithNullableMember _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithNullableMember
                {
                    TestInt = 12345,
                    TestString = "Test String",
                    TestDateTime = DateTime.Now.Date
                }.ToExpectedObject();

                _actual = new TypeWithNullableMember
                {
                    TestInt = 12345,
                    TestString = "Test String",
                    TestDateTime = DateTime.Now.Date,
                    TestNullDateTime = new DateTime(2000, 01, 01)
                };
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_ignore_nullable_types = () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_014);
        }

        class when_comparing_equal_types_with_differing_ignored_nullable_members
        {
            static ExpectedObject _expected;
            static TypeWithNullableMember _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithNullableMember
                {
                    TestInt = 12345,
                    TestString = "Test String",
                    TestDateTime = DateTime.Now.Date
                }.ToExpectedObject(x => x.Ignore(i => i.TestNullDateTime));

                _actual = new TypeWithNullableMember
                {
                    TestInt = 12345,
                    TestString = "Test String",
                    TestDateTime = DateTime.Now.Date,
                    TestNullDateTime = DateTime.Now
                };
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_ignore_nullable_types = () => _exception.ShouldBeNull();
        }

        [Subject("Nulls")]
        class when_comparing_nulls
        {
            static bool _result;

            Because of = () => _result = ((object) null).ToExpectedObject().Equals((object) null);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Nulls")]
        class when_compariong_object_with_null_property
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _result;

            Establish context = () =>
            {
                _expected = new
                {
                    TypeWithString = new {StringProperty = "test"}
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
}