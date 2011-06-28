using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_equating_expected_object_to_unequal_actual_object
    {
        static ComplexType _actual;
        static Exception _exception;
        static ExpectedObjects.ExpectedObject _expected;

        Establish context = () =>
            {
                _expected = new ComplexType
                                {
                                    StringProperty = "test string",
                                    TypeWithString = new TypeWithString
                                                         {
                                                             StringProperty = "inner test string"
                                                         }
                                }.ToExpectedObject();

                _actual = new ComplexType
                              {
                                  StringProperty = "test string1",
                                  TypeWithString = new TypeWithString
                                                       {
                                                           StringProperty = "inner test string2"
                                                       }
                              };
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));

        It should_throw_exception_with_error_for_StringProperty =
            () =>
            _exception.Message.ShouldContain("For ComplexType.StringProperty, expected \"test string\" but found \"test string1\".");

        It should_throw_exception_with_error_for_TypeWithString_StringProperty =
            () =>
            _exception.Message.ShouldContain("For ComplexType.TypeWithString.StringProperty, expected \"inner test string\" but found \"inner test string2\".");
    }
}