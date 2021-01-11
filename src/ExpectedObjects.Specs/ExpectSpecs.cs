using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class ExpectSpecs
    {
        [Subject("Expect Not Null")]
        class when_comparing_type_with_not_null_expectation_to_matching_instance
        {
            static ExpectedObject _expected;
            static TypeWithString _actual;

            Establish context = () =>
            {
                _expected = new {StringProperty = Expect.NotNull()}.ToExpectedObject();
                _actual = new TypeWithString {StringProperty = "anything"};
            };

            It should_match = () => _expected.ShouldMatch(_actual);
        }

        [Subject("Expect Not Null")]
        class when_comparing_array_with_not_null_expectations_to_matching_instance
        {
            static ExpectedObject _expected;
            static TypeWithString[] _actual;

            Establish context = () =>
            {
                _expected = new[] {new {StringProperty = Expect.NotNull()}}.ToExpectedObject();
                _actual = new[] {new TypeWithString {StringProperty = "anything"}};
            };

            It should_match = () => _expected.ShouldMatch(_actual);
        }

        [Subject("Expect Default")]
        class when_comparing_int_with_default
        {
            static ExpectedObject _expected;
            static TypeWithInteger _actual;

            Establish context = () =>
            {
                _expected = new {IntegerProperty = Expect.Default<int>()}.ToExpectedObject();
                _actual = new TypeWithInteger {IntegerProperty = 0};
            };

            It should_match = () => _expected.ShouldMatch(_actual);
        }

        [Subject("Expect Not Default")]
        class when_comparing_int_with_not_default
        {
            static ExpectedObject _expected;
            static TypeWithInteger _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new {IntegerProperty = Expect.NotDefault<int>()}.ToExpectedObject();
                _actual = new TypeWithInteger {IntegerProperty = 42};
            };

            Because of = () => _results = _expected.Matches(_actual);

            It should_match = () => _results.ShouldBeTrue();
        }

        [Subject("Expect Not Default")]
        class when_comparing_any_int_with_missing_property
        {
            static ExpectedObject _expected;
            static bool _results;

            Establish context = () =>
            {
                _expected = new { IntegerProperty = Expect.Any<int>() }.ToExpectedObject();
            };

            Because of = () => _results = _expected.Matches(new { Something = "Nothing"});

            It should_not_match = () => _results.ShouldBeFalse();
        }

        [Subject("Expect Not Default")]
        class when_comparing_default_int_with_not_default
        {
            static ExpectedObject _expected;
            static TypeWithInteger _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new { IntegerProperty = Expect.NotDefault<int>() }.ToExpectedObject();
                _actual = new TypeWithInteger { IntegerProperty = 0 };
            };

            Because of = () => _results = _expected.Matches(_actual);

            It should_match = () => _results.ShouldBeFalse();
        }

        [Subject("Expect Default")]
        class when_comparing_anonymous_type_with_expected_default_int_to_type_with_decimal
        {
            static ExpectedObject _expected;
            static TypeWithDecimal _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new {DecimalProperty = Expect.Default<int>()}.ToExpectedObject();
                _actual = new TypeWithDecimal {DecimalProperty = 0};
            };

            Because of = () => _results = _expected.Matches(_actual);

            It should_not_match = () => _results.ShouldBeFalse();
        }
    }
}