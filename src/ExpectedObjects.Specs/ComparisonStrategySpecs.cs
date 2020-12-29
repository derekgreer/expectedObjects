using System;
using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using ExpectedObjects.Strategies;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{
    [Subject("Strategies")]
    public class when_pushing_a_strategy
    {
        static TypeWithIEnumerable _actual;
        static Mock<IComparisonStrategy> _comparisonStrategyMock;
        static ExpectedObject _expected;

        static bool _result;

        Establish context = () =>
        {
            _comparisonStrategyMock = new Mock<IComparisonStrategy>();
            _comparisonStrategyMock.Setup(x => x.CanCompare(Moq.It.IsAny<object>(), Moq.It.IsAny<object>())).Returns(true);
            _comparisonStrategyMock.Setup(
                    x => x.AreEqual(Moq.It.IsAny<object>(), Moq.It.IsAny<object>(), Moq.It.IsAny<IComparisonContext>()))
                .Returns(false);

            _expected = new TypeWithIEnumerable {Objects = new List<string> {"test string"}}
                .ToExpectedObject(ctx => ctx.UseStrategies(new List<IComparisonStrategy> {_comparisonStrategyMock.Object}));
            _actual = new TypeWithIEnumerable {Objects = new List<string> {"test string"}};
        };

        Because of = () => _result = _expected.Equals(_actual);

        It should_use_the_strategy = () => _result.ShouldBeFalse();
    }
}