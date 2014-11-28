using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{
    public class DtoMapperSpecs
    {
        private static ComplexType _actual;
        private static object _expected;
        private static bool _called;

        private static bool _result;
        private static Mock<IComparison> _comparisionSpy;

        private Establish context = () =>
        {
            _comparisionSpy = new Mock<IComparison>();
            _comparisionSpy.Setup(x => x.AreEqual(Moq.It.IsAny<object>()))
                .Callback<object>(x => _called = true);

            _expected = new
            {
                StringProperty = _comparisionSpy.Object
            }.ToExpectedObject(true);

            _actual = new ComplexType
            {
                StringProperty = "test string",
                DecimalProperty = 10.10m,   
                IndexType = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})
            };
        };

        private Because of = () => _result = _expected.Equals(_actual);

        private It should_use_custom_comparison = () => _called.ShouldBeTrue();
    }
}

