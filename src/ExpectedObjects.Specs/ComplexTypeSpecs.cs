using System;
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

    [Subject("Complex Type")]
    public class when_comparing_unequal_response_types_with_writer
    {
        static ExpectedObject _expected;
        static Response _actual;

        Establish context = () =>
        {
            _expected = new
            {
                Id = Expect.Any<int>(),
                Description = "Desc",
                IsInStock = false,
                Quantity = 4L,
                CreatedDate = Expect.Any<DateTime>()
            }.ToExpectedObject();

            _actual = new Response
            {
                Id = 1,
                Description = "Desc",
                IsInStock = true,  //expected: false
                Quantity = null,  //expected: 4
                CreatedDate = DateTime.Now
            };
        };

        It should_show_results = Create.Observation(() => () => _expected.ShouldMatch(_actual));
    }

    public class Response
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsInStock { get; set; }
        public object Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}