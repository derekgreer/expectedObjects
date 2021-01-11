using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class ShouldNotEqualExtensionSpecs
    {
        [Subject("ShouldNotEqual Extensions")]
        class when_comparing_enumerables_of_complex_types_with_different_values_for_inequality
        {
            static TypeWithIEnumerable _actual;
            static TypeWithIEnumerable _expected;

            Establish context = () =>
            {
                _expected = new TypeWithIEnumerable
                {
                    Objects = new List<ComplexType>
                    {
                        new()
                        {
                            StringProperty = "test1",
                            DecimalProperty = 10.0m,
                            TypeWithString = new TypeWithString {StringProperty = "value1"}
                        },
                        new()
                        {
                            StringProperty = "test1",
                            DecimalProperty = 10.0m,
                            TypeWithString = new TypeWithString {StringProperty = "value1"}
                        }
                    }
                };

                _actual = new TypeWithIEnumerable
                {
                    Objects = new List<ComplexType>
                    {
                        new()
                        {
                            StringProperty = "test2",
                            DecimalProperty = 11.0m,
                            TypeWithString = new TypeWithString {StringProperty = "value2"}
                        },
                        new()
                        {
                            StringProperty = "test2",
                            DecimalProperty = 11.0m,
                            TypeWithString = new TypeWithString {StringProperty = "value2"}
                        }
                    }
                };
            };

            It should_not_be_equal = () => _expected.ToExpectedObject().ShouldNotEqual(_actual);
        }
    }
}