using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_comparing_unequal_objects_with_an_index
    {
        static IndexType<int> _actual;
        static IndexType<int> _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new IndexType<int>(new List<int> {1, 2, 3, 4, 5});
                _expected = new IndexType<int>(new List<int> { 1, 2, 3, 4, 6 });
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    public class when_comparing_unequal_objects_with_two_indexes
    {
        static MultiIndexType _actual;
        static MultiIndexType _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new MultiIndexType
                              {
                                  IndexType1 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 5 }),
                                  IndexType2 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 6 })
                              };

                _expected = new MultiIndexType
                                {
                                    IndexType1 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 6 }),
                                    IndexType2 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 7 })
                                };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    public class when_comparing_equal_objects_with_an_index
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

    public class when_comparing_equal_objects_with_two_indexes
    {
        static MultiIndexType _actual;
        static MultiIndexType _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new MultiIndexType
                              {
                                  IndexType1 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 5 }),
                                  IndexType2 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 6 })

                              };

                _expected = new MultiIndexType
                                {
                                    IndexType1 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 5 }),
                                    IndexType2 = new IndexType<int>(new List<int> { 1, 2, 3, 4, 6 })
                                };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeTrue();
    }
}