using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    [Subject("Enumerables")]
    public class when_comparing_unequal_enumerables
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;

        static bool _result;

        Establish context = () =>
        {
            _expected = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2"}};
            _actual = new TypeWithIEnumerable {Objects = new List<string> {"test3", "test4"}};
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    [Subject("Enumerables")]
    public class when_comparing_unequal_enumerables_with_duplicates
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;

        static bool _result;

        Establish context = () =>
        {
            _expected = new TypeWithIEnumerable { Objects = new List<string> { "test2", "test2", "test2", "test2" } };
            _actual = new TypeWithIEnumerable { Objects = new List<string> { "test2" } };
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    [Subject("Enumerables")]
    public class when_comparing_unequal_enumerables_with_duplicates_in_actual
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;

        static bool _result;

        Establish context = () =>
        {
            _expected = new TypeWithIEnumerable { Objects = new List<string> { "test2"} };
            _actual = new TypeWithIEnumerable { Objects = new List<string> { "test2", "test2", "test2", "test2" } };
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    [Subject("Enumerables")]
    public class when_comparing_equal_enumerables
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;

        static bool _result;

        Establish context = () =>
        {
            _expected = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2"}};
            _actual = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2"}};
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    [Subject("Enumerables")]
    public class when_comparing_enumerables_with_fewer_actual_values
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;
        static bool _result;

        Establish context = () =>
        {
            _expected = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2", "test3"}};
            _actual = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2"}};
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    [Subject("Enumerables")]
    public class when_comparing_equal_enumerables_with_different_order_elements_with_default_configuration
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;
        static bool _result;

        Establish context = () =>
        {
            _expected = new TypeWithIEnumerable {Objects = new List<string> {"test2", "test1"}};
            _actual = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2"}};
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    [Subject("Enumerables")]
    public class when_comparing_types_with_unequal_enumerables_with_different_order_elements_with_default_configuration
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;
        static bool _result;

        Establish context = () =>
        {
            _expected = new TypeWithIEnumerable
            {
                Objects = new List<ComplexType>
                {
                    new ComplexType {DecimalProperty = 1.1m},
                    new ComplexType {DecimalProperty = 1.2m}
                }
            };
            _actual = new TypeWithIEnumerable
            {
                Objects = new List<ComplexType>
                {
                    new ComplexType {DecimalProperty = 1.3m},
                    new ComplexType
                    {
                        DecimalProperty = 1.4m,
                        TypeWithIEnumerable = new TypeWithIEnumerable
                        {
                            Objects = new List<string> {"test", "test"}
                        }
                    }
                }
            };
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    [Subject("Enumerables")]
    public class when_comparing_unequal_enumerables_with_different_order_elements_with_default_configuration
    {
        static List<TypeWithDecimal> _actual;
        static List<TypeWithDecimal> _expected;
        static bool _result;

        Establish context = () =>
        {
            _expected = new List<TypeWithDecimal>
            {
                new TypeWithDecimal {DecimalProperty = 1.1m},
                new TypeWithDecimal {DecimalProperty = 1.2m}
            };
            _actual = new List<TypeWithDecimal>
            {
                new TypeWithDecimal {DecimalProperty = 1.3m},
                new TypeWithDecimal {DecimalProperty = 1.4m}
            };
        };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}