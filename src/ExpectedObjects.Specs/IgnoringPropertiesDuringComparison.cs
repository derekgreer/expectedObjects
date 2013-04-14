using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class IgnoringPropertiesDuringComparison
    {
        static ComplexType _actual;
        static ComplexType _expected;
        static Exception _exception;

        Establish context = () =>
                                {
                                    _actual = new ComplexType
                                                  {
                                                      DecimalProperty = 4,
                                                      IntegerProperty = 5,
                                                      StringProperty = "foo",
                                                      TypeWithString =
                                                          new TypeWithString {StringProperty = "bar"}
                                                  };

                                    _expected = new ComplexType
                                                  {
                                                      DecimalProperty = 4,
                                                      IntegerProperty = 5,
                                                      StringProperty = "foo",
                                                      TypeWithString =
                                                          new TypeWithString {StringProperty = "baz"}
                                                  };
                                };

        Because of = () => _exception = Catch.Exception(() => _actual.IsExpectedToBeLike(_expected, x => x.TypeWithString.StringProperty));

        It should_not_throw_an_exception = () => _exception.ShouldBeNull();
    }
}