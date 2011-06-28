using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_comparing_types_with_enumerables_with_different_values
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

    public class when_comparing_equal_enumerables
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;

        static bool _result;

        Establish context = () =>
            {
                _expected = new TypeWithIEnumerable { Objects = new List<string> { "test1", "test2" } };
                _actual = new TypeWithIEnumerable { Objects = new List<string> { "test1", "test2" } };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeTrue();
    }

    public class when_comparing_an_enumerable_with_fewer_values
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;

        static bool _result;

        Establish context = () =>
            {
                _expected = new TypeWithIEnumerable { Objects = new List<string> { "test1", "test2", "test3" } };
                _actual = new TypeWithIEnumerable { Objects = new List<string> { "test1", "test2" } };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}