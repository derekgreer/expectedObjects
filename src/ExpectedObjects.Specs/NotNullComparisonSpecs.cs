using System;
using System.Collections.Generic;
using ExpectedObjects.Comparisons;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_asserting_equality_for_equal_not_null_comparison
    {
        static ComplexType _actual;
        static ExpectedObject _expected;
        static Exception _exception;

        Establish context = () =>
        {
            _expected = new
            {
                StringProperty = new NotNullComparison()
            }.ToExpectedObject();

            _actual = new ComplexType
            {
                StringProperty = "a string",
                DecimalProperty = 10.10m,
                IndexType = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})
            };
        };

        Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));


        It should_not_throw_an_expection = () => _exception.ShouldBeNull();
    }

    public class when_comparing_not_null_property_with_not_null_comparison
    {
        static ComplexType _actual;
        static ExpectedObject _expected;
        static bool _result;


        Establish context = () =>
        {
            _expected = new
            {
                StringProperty = new NotNullComparison()
            }.ToExpectedObject();

            _actual = new ComplexType
            {
                StringProperty = "test string",
                DecimalProperty = 10.10m,
                IndexType = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})
            };
        };

        Because of = () => _result = _expected.Matches(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }
}