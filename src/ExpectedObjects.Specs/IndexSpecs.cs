using System;
using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class IndexSpecs
    {
        [Subject("Indexes")]
        class when_comparing_unequal_objects_with_an_index
        {
            static IndexType<int> _actual;
            static IndexType<int> _expected;

            static bool _result;

            Establish context = () =>
            {
                _actual = new IndexType<int>(new List<int> {1, 2, 3, 4, 5});
                _expected = new IndexType<int>(new List<int> {1, 2, 3, 4, 6});
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }

        [Subject("Indexes")]
        class when_comparing_unequal_objects_with_two_indexes
        {
            static MultiIndexType _actual;
            static MultiIndexType _expected;

            static bool _result;

            Establish context = () =>
            {
                _actual = new MultiIndexType
                {
                    IndexType1 = new IndexType<int>(new List<int> {1, 2, 3, 4, 5}),
                    IndexType2 = new IndexType<string>(new List<string> {"1", "2"})
                };

                _expected = new MultiIndexType
                {
                    IndexType1 = new IndexType<int>(new List<int> {1, 2, 3, 4, 6}),
                    IndexType2 = new IndexType<string>(new List<string> {"1", "3"})
                };
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }

        [Subject("Indexes")]
        class when_comparing_equal_objects_with_an_index
        {
            static IndexType<int> _actual;
            static IndexType<int> _expected;

            static bool _result;

            Establish context = () =>
            {
                _expected = new IndexType<int>(new List<int> {1, 2, 3, 4, 5});
                _actual = new IndexType<int>(new List<int> {1, 2, 3, 4, 5});
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Indexes")]
        class when_comparing_equal_objects_with_two_indexes
        {
            static MultiIndexType _actual;
            static MultiIndexType _expected;

            static bool _result;

            Establish context = () =>
            {
                _actual = new MultiIndexType
                {
                    IndexType1 = new IndexType<int>(new List<int> {1, 2, 3, 4, 5}),
                    IndexType2 = new IndexType<string>(new List<string> {"1", "2"})
                };

                _expected = new MultiIndexType
                {
                    IndexType1 = new IndexType<int>(new List<int> {1, 2, 3, 4, 5}),
                    IndexType2 = new IndexType<string>(new List<string> {"1", "2"})
                };
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_not_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Indexes")]
        class when_comparing_equal_objects_with_two_indexes_with_the_same_name
        {
            static TypeWithOverloadedIndexes<int> _actual;
            static TypeWithOverloadedIndexes<int> _expected;

            static bool _result;

            Establish context = () =>
            {
                _actual = new TypeWithOverloadedIndexes<int>(new List<int> {1});
                _expected = new TypeWithOverloadedIndexes<int>(new List<int> {1});
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Indexes")]
        class when_comparing_unequal_objects_with_two_indexes_with_the_same_name
        {
            static TypeWithOverloadedIndexes<int> _actual;
            static TypeWithOverloadedIndexes<int> _expected;

            static bool _result;

            Establish context = () =>
            {
                _actual = new TypeWithOverloadedIndexes<int>(new List<int> { 1 });
                _expected = new TypeWithOverloadedIndexes<int>(new List<int> { 2 });
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }

        [Subject("Indexes")]
        class when_comparing_unequal_types_which_override_indexes
        {
            static ExpectedObject _expected;
            static TypeWithOverloadedIndexes<int> _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithOverloadedIndexes<int>(new List<int> { 2 }).ToExpectedObject();
                _actual = new TypeWithOverloadedIndexes<int>(new List<int> { 1 });
            };

            Because of = () => _exception = Catch.Exception(() => _expected.Equals(_actual));

            It should_not_be_equal = () => _expected.ShouldNotEqual(_actual);

            It should_not_throw_an_exception = () => _exception.ShouldBeNull();
        }
    }
}