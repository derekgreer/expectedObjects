using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class setting_properties_to_default_values
    {
        static SimpleType _item;

        Establish context = () =>
                                {
                                    _item = new SimpleType {IntProperty = 3,StringProperty = "bar"};
                                };

        Because of = () => _item.SetToDefault(x => x.IntProperty, x=> x.StringProperty);

        It should_have_a_zero_for_int_property = () => _item.IntProperty.ShouldEqual(0);

        It should_have_a_null_for_string_property = () => _item.StringProperty.ShouldBeNull();
    }

    public class setting_nested_properties_to_default_values
    {
        static ComplexType _item;

        Establish context = () =>
                                {
                                    _item = new ComplexType {TypeWithString = new TypeWithString {StringProperty = "foo"}};
                                };

        Because of = () => _item.SetToDefault(x=> x.TypeWithString.StringProperty);
        
        It should_have_a_null_for_string_property = () => _item.TypeWithString.StringProperty.ShouldBeNull();
    }
}