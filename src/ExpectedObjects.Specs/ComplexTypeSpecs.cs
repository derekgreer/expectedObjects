using System.Dynamic;
using ExpectedObjects.Specs.Infrastructure;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    [Subject("Complex Type")]
    public class when_comparing_unequal_complex_types_with_writer
    {
        static ExpectedObject _expected;
        static object _actual;

        Establish context = () =>
        {
            _expected = new
            {
                Level1 = new
                {
                    Level2 = new
                    {
                        Level3 = "test1"
                    }
                }
            }.ToExpectedObject();

            _actual = new
            {
                Level1 = new
                {
                    Level2 = new
                    {
                        Level3 = "test2"
                    }
                }
            };
        };

       It should_show_results = Create.Observation(() => () =>_expected.ShouldMatch(_actual));
    }
}