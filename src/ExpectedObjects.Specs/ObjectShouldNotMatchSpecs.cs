using System;
using ExpectedObjects;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

public class when_comparing_different_types_with_same_member_for_equality_with_actual_as_subject
{
    static TypeWithString2 _actual;
    static Exception _exception;
    static ExpectedObject _expected;

    Establish context = () =>
    {
        _expected = new TypeWithString
        {
            StringProperty = "this is a test"
        }.ToExpectedObject();

        _actual = new TypeWithString2
        {
            StringProperty = "this is a test"
        };
    };

    Because of = () => _exception = Catch.Exception(() => _actual.ShouldEqual(_expected));

    It should_throw_exception_with_TypeWithString_message = () => _exception.Message.ShouldEqual(
        string.Format("For TypeWithString2, expected {0} but found {1}.{2}",
                      typeof(TypeWithString).FullName,
                      typeof(TypeWithString2).FullName,
                      Environment.NewLine));
}