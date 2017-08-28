using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    [Subject("Recursion")]
    public class when_partially_comparing_equal_types_with_recursion
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
    public class when_comparing_equal_types_with_duplicate_references
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
    public class when_comparing_types_for_match_with_unequal_recursive_values
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
    public class when_comparing_equal_types_with_recursion
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
    public class when_comparing_unequal_types_with_recursion
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
}