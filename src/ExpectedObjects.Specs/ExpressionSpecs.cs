using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class ExpressionSpecs
    {
        [Subject("Expressions")]
        class when_comparing_types_with_unequal_expressions
        {
            static TypeWithExpression<string> _actual;
            static ExpectedObject _expected;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithExpression<string>[]
                {
                    new TypeWithExpression<string>(x => x)
                }.ToExpectedObject();

                _actual = new TypeWithExpression<string>(x => x.Length);
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();
        }
    }
}