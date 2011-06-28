using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_comparing_objects_of_different_types_with_different_members
    {
        static TypeWithString _actual;
        static TypeWithDecimal _expected;
        static bool _result;

        Establish context = () =>
            {
                _expected = new TypeWithDecimal {DecimalProperty = 10.0m};

                _actual = new TypeWithString {StringProperty = "this is a test"};
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    public class when_comparing_equal_objects_of_different_types_ignoring_types
    {
        static TypeWithString _expected;
        static TypeWithString2 _actual;
        static bool _result;

        Establish context = () =>
            {
                _expected = new TypeWithString()
                                {
                                    StringProperty = "this is a test"
                                };

                _actual = new TypeWithString2
                              {
                                  StringProperty = "this is a test"
                              };
            };

        Because of = () => _result = _expected.ToExpectedObject().IgnoreTypes().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}