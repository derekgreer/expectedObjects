using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class MemberTypeConfigurationSpecs
    {
        [Subject("Configuring Members")]
        class when_comparing_unequal_objects_with_string_field_configured_for_fields
        {
            static TypeWithStringField _actual;
            static ExpectedObject _expected;
            static bool _result;

            Establish context = () =>
            {
                _expected = new TypeWithStringField {StringField = "test"}.ToExpectedObject(ctx =>
                    ctx.IncludeMemberTypes(MemberType.PublicFields));

                _actual = new TypeWithStringField {StringField = "test2"};
            };

            Because of = () => _result = _expected.Equals(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }
    }
}