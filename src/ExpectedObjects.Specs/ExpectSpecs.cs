using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class ExpectSpecs
    {
        [Subject("Expect Not Null")]
        public class when_comparing_type_with_not_null_expectation_to_matching_instance
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
        public class when_comparing_array_with_not_null_expectations_to_matching_instance
        {
            static ExpectedObject _expected;
            static TypeWithString[] _actual;

            Establish context = () =>
            {
                _expected = new [] { new {StringProperty = Expect.NotNull()} }.ToExpectedObject();
                _actual = new TypeWithString[] { new TypeWithString{ StringProperty = "anything"}};
            };

            It should_match = () => _expected.ShouldMatch(_actual);
        }
    }
}