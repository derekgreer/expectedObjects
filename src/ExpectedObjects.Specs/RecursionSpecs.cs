using System;
using ExpectedObjects.Specs.Properties;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class RecursionSpecs
    {
        [Subject("Recursion")]
        class when_partially_comparing_equal_types_with_recursion
        {
            static object _expected;
            static RecursiveType _actual;
            static bool _result;

            Establish context = () =>
            {
                _expected = new
                {
                    Value = 1,
                    Type = new RecursiveType(1)
                };

                _actual = new RecursiveType(1);
                _actual.Type = _actual;
            };

            Because of = () => _result = _expected.ToExpectedObject().Matches(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Recursion")]
        class when_comparing_equal_types_with_duplicate_references
        {
            static DuplicateReferenceType _actual;
            static bool _result;
            static object _expected;

            Establish context = () =>
            {
                var simpleType = new SimpleType {IntProperty = 1};
                _expected = new DuplicateReferenceType {Type1 = simpleType, Type2 = simpleType};

                _actual = new DuplicateReferenceType {Type1 = simpleType, Type2 = simpleType};
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }


        [Subject("Recursion")]
        class when_comparing_types_for_match_with_unequal_recursive_values
        {
            static RecursiveType _actual;
            static bool _result;
            static object _expected;

            Establish context = () =>
            {
                _expected = new
                {
                    Value = 1,
                    Type = new
                    {
                        Value = 1,
                        Type = new RecursiveType(2) // this won't ever be compared
                    }
                };

                _actual = new RecursiveType(1);
                _actual.Type = _actual;
            };

            Because of = () => _result = _expected.ToExpectedObject().Matches(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Recursion")]
        class when_comparing_equal_types_with_recursion
        {
            static RecursiveType _expected;
            static RecursiveType _actual;
            static bool _result;

            Establish context = () =>
            {
                _expected = new RecursiveType(1);
                _expected.Type = _expected;
                _actual = new RecursiveType(1);
                _actual.Type = _actual;
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_not_compare_the_same_objects_more_than_once = () => _expected.Access.ShouldEqual(1);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Recursion")]
        class when_comparing_unequal_types_with_recursion
        {
            static RecursiveType _expected;
            static RecursiveType _actual;
            static bool _result;

            Establish context = () =>
            {
                _expected = new RecursiveType(1);
                _expected.Type = _expected;
                _actual = new RecursiveType(2);
                _actual.Type = _actual;
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_not_compare_the_same_objects_more_than_once = () => _expected.Access.ShouldEqual(1);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }

        [Subject("Recursion")]
        class when_comparing_different_objects_with_array_properties_with_items_that_have_back_reference_to_parent_object
        {
            static object _actual;
            static ExpectedObject _expected;
            static Exception _exception;

            Establish context = () =>
            {
                var expected = new {Array = new object[1]};
                expected.Array[0] = new {Parent = expected, Id = 1};

                _expected = expected.ToExpectedObject();

                var actual = new {Array = new object[1]};
                actual.Array[0] = new {Parent = actual, Id = 2};

                _actual = actual;
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();

            It should_have_the_expected_exception_message =
                () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_002);
        }

        [Subject("Recursion")]
        class when_comparing_ordinal_different_objects_with_array_properties_with_items_that_have_back_reference_to_parent_object
        {
            static object _actual;
            static ExpectedObject _expected;
            static Exception _exception;

            Establish context = () =>
            {
                var expected = new { Array = new object[1] };
                expected.Array[0] = new { Parent = expected, Id = 1 };

                _expected = expected.ToExpectedObject(ctx => ctx.UseOrdinalComparison());

                var actual = new { Array = new object[1] };
                actual.Array[0] = new { Parent = actual, Id = 2 };

                _actual = actual;
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();

            It should_have_the_expected_exception_message =
                () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_005);
        }
    }
}