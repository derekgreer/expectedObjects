using System;
using ExpectedObjects.Specs.Properties;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    [Subject("Complex Type")]
    public class when_comparing_unequal_complex_types_with_writer
    {
        static ExpectedObject _expected;
        static object _actual;
        static Exception _exception;

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
                },
                StringProperty = "test1"
            }.ToExpectedObject();

            _actual = new
            {
                Level1 = new
                {
                    Level2 = new
                    {
                        Level3 = "test2"
                    }
                },
                StringProperty = "test2"
            };
        };

        Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

        It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();

        It should_have_the_expected_exception_message =
            () => _exception.Message.ShouldEqual(Resources.when_comparing_unequal_complex_types_with_writer_should_have_the_expected_exception_message);
    }

    [Subject("Complex Type")]
    public class when_comparing_unequal_response_types_with_writer
    {
        static ExpectedObject _expected;
        static Response _actual;
        static Exception _exception;

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
                IsInStock = true,
                Quantity = null,
                CreatedDate = DateTime.Now
            };
        };

        Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

        It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();

        It should_have_the_expected_exception_message =
            () => _exception.Message.ShouldEqual(Resources
                .when_comparing_unequal_response_types_with_writer_context);
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