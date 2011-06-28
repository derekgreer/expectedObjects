using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_comparing_unequal_enumerables
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;

        static bool _result;

        Establish context = () =>
            {
                _expected = new TypeWithIEnumerable { Objects = new List<string> { "test1", "test2" } };
                _actual = new TypeWithIEnumerable { Objects = new List<string> { "test3", "test4" } };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }
}