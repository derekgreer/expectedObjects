using System;
using ExpectedObjects.Specs.Properties;
using Machine.Specifications;
using ExpectedObjects.Specs.Extensions;
using ExpectedObjects.Specs.TestTypes;

namespace ExpectedObjects.Specs
{
    class StaticTypeSpecs
    {
        [Subject("Static Member Comparisons")]
        class when_comparing_unequal_types_with_static_members
        {
            static ExpectedObject _expected;
            static TypeWithTypeSafeEnumArray _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithTypeSafeEnumArray { Id = 10, Enums = new[] { new TypeWithTypeSafeEnumField { Id = 1 } } }.ToExpectedObject();
                _actual = new TypeWithTypeSafeEnumArray { Id = 10, Enums = new[] { new TypeWithTypeSafeEnumField { Id = 10 } } };
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));

            It should_have_the_expected_exception_message = () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_016);
        }
    }
}