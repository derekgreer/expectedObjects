using System;
using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_asserting_equality_for_indexes_with_different_values
    {
        static TypeWithIndexType _actual;
        static Exception _exception;
        static TypeWithIndexType _expected;

        Establish context = () =>
            {
                _expected = new TypeWithIndexType {Ints = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})};
                _actual = new TypeWithIndexType {Ints = new IndexType<int>(new List<int> {1, 2, 3, 4, 6})};
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual(_actual));

        It should_throw_exception_with_subscripted_values =
            () =>
            _exception.Message.ShouldEqual(
                string.Format("For TypeWithIndexType.Ints.Item[4], expected [5] but found [6].{0}", Environment.NewLine));
    }

    public class when_asserting_equality_for_types_with_indexes_with_different_values
    {
        static IndexType<int> _actual;
        static Exception _exception;
        static IndexType<int> _expected;

        Establish context = () =>
            {
                _expected = new IndexType<int>(new List<int> {1, 2, 3, 4, 5});
                _actual = new IndexType<int>(new List<int> {1, 2, 3, 4, 6});
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual(_actual));

        It should_throw_exception_with_subscripted_values =
            () =>
            _exception.Message.ShouldEqual(
                string.Format("For IndexType`1.Item[4], expected [5] but found [6].{0}", Environment.NewLine));
    }
}