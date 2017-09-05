using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class ExclusionSpecs
    {
        [Subject("Ignore")]
        public class when_excluding_a_property
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "test"}
                }.ToExpectedObject(ctx => ctx.Ignore(x => x.TypeWithString.StringProperty));

                _actual = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {  StringProperty = "different"}
                };
            };

            Because of = () => _results = _expected.Equals(_actual);

            It should_ignore_differences_in_property = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore")]
        public class when_excluding_a_property_with_anonymous_expected_objects
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString { StringProperty = "test" }
                }.ToExpectedObject(ctx => ctx.Ignore(x => x.TypeWithString.StringProperty));

                _actual = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString { StringProperty = "different" }
                };
            };

            Because of = () => _results = _expected.Matches(_actual);

            It should_ignore_differences_in_property = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore")]
        public class when_excluding_a_base_type
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString { StringProperty = "test" }
                }.ToExpectedObject(ctx => ctx.Ignore(x => x));

                _actual = new ComplexType
                {
                    StringProperty = "level 2",
                    TypeWithString = new TypeWithString { StringProperty = "different" }
                };
            };

            Because of = () => _results = _expected.Equals(_actual);

            It should_ignore_differences = () => _results.ShouldBeTrue();
        }
    }
}