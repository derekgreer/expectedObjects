using System;
using System.Collections.Generic;
using ExpectedObjects.Specs.Properties;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class EnumerableSpecs
    {
        [Subject("Enumerables")]
        class when_comparing_unequal_enumerables
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
        class when_comparing_unequal_enumerables_with_duplicates
        {
            static TypeWithIEnumerable _actual;
            static TypeWithIEnumerable _expected;

            static bool _result;

            Establish context = () =>
            {
                _expected = new TypeWithIEnumerable {Objects = new List<string> {"test2", "test2", "test2", "test2"}};
                _actual = new TypeWithIEnumerable {Objects = new List<string> {"test2"}};
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }

        [Subject("Enumerables")]
        class when_comparing_unequal_enumerables_with_duplicates_in_actual
        {
            static TypeWithIEnumerable _actual;
            static TypeWithIEnumerable _expected;

            static bool _result;

            Establish context = () =>
            {
                _expected = new TypeWithIEnumerable {Objects = new List<string> {"test2"}};
                _actual = new TypeWithIEnumerable {Objects = new List<string> {"test2", "test2", "test2", "test2"}};
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }

        [Subject("Enumerables")]
        class when_comparing_equal_enumerables
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
        class when_comparing_enumerables_with_fewer_actual_values
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
        class when_comparing_equal_enumerables_with_different_order_elements_with_default_configuration
        {
            static TypeWithIEnumerable _actual;
            static ExpectedObject _expected;
            static bool _result;

            Establish context = () =>
            {
                _expected = new TypeWithIEnumerable {Objects = new List<string> {"test2", "test1"}}
                    .ToExpectedObject();

                _actual = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2"}};
            };

            Because of = () => _result = _expected.Equals(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();

            It should_show_results = () => _expected.ShouldMatch(_actual);
        }

        [Subject("Enumerables")]
        class when_comparing_equal_enumerables_with_different_order_elements_with_ordinal_configuration
        {
            static ExpectedObject _expected;
            static TypeWithIEnumerable _actual;
            static bool _result;

            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithIEnumerable {Objects = new List<string> {"test2", "test1"}}
                    .ToExpectedObject(ctx => ctx.UseOrdinalComparison());

                _actual = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2"}};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();

            It should_have_the_expected_exception_message =
                () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_003);
        }

        [Subject("Enumerables")]
        class when_comparing_types_with_unequal_enumerables_with_different_order_elements_with_default_configuration
        {
            static ExpectedObject _expected;
            static TypeWithIEnumerable _actual;
            static bool _result;

            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithIEnumerable
                {
                    Objects = new List<ComplexType>
                    {
                        new ComplexType {DecimalProperty = 1.1m},
                        new ComplexType {DecimalProperty = 1.2m}
                    }
                }.ToExpectedObject();

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

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();

            It should_have_the_expected_exception_message =
                () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_006);
        }

        [Subject("Enumerables")]
        class when_comparing_unequal_enumerables_with_different_order_elements_with_default_configuration
        {
            static List<TypeWithDecimal> _actual;
            static ExpectedObject _expected;
            static bool _result;

            static Exception _exception;

            Establish context = () =>
            {
                _expected = new List<TypeWithDecimal>
                {
                    new TypeWithDecimal {DecimalProperty = 1.1m},
                    new TypeWithDecimal {DecimalProperty = 1.2m}
                }.ToExpectedObject();

                _actual = new List<TypeWithDecimal>
                {
                    new TypeWithDecimal {DecimalProperty = 1.3m},
                    new TypeWithDecimal {DecimalProperty = 1.4m}
                };
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();

            It should_have_the_expected_exception_message =
                () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_008);
        }

        [Subject("Enumerables")]
        class when_comparing_objects_with_array_properties_that_have_null_items_and_do_not_match
        {
            static object _actual;
            static object _expected;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new {Array = new[] {"1", null}};
                _actual = new {Array = new string[] {null, null}};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldMatch(_actual));

            It should_not_throw_an_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();
        }
    }
}