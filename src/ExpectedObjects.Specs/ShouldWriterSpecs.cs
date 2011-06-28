using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_retrieving_formatted_result_from_unequal_comparision
    {
        static EqualityResult _result;
        static IWriter _writer;

        Establish context = () =>
            {
                _writer = new ShouldWriter();
                _result = new EqualityResult(false, "StringProperty", 1, 2);
            };

        Because of = () => _writer.Write(_result);

        It should_return_formatted_result =
            () => _writer.GetFormattedResults().ShouldEqual("For StringProperty, expected [1] but found [2]." + System.Environment.NewLine);
    }

    public class when_retrieving_formatted_result_from_composite_comparision
    {
        static string _results;
        static IWriter _writer;

        Establish context = () =>
            {
                _writer = new ShouldWriter();
                _writer.Write(new EqualityResult(false, "ContainingObject.StringProperty", 1, 2));
                _writer.Write(new EqualityResult(false, "ContainingObject", 1, 2));
            };

        Because of = () => _results = _writer.GetFormattedResults();

        It should_not_contain_error_for_composing_object = () => _results.ShouldNotContain("ContainingObject:");
    }

    public class when_retrieving_formatted_result_from_equal_comparision
    {
        static EqualityResult _result;
        static IWriter _writer;

        Establish context = () =>
            {
                _writer = new ShouldWriter();
                _result = new EqualityResult(true, "StringProperty", 1, 2);
            };

        Because of = () => _writer.Write(_result);

        It should_not_return_results =
            () => _writer.GetFormattedResults().ShouldBeEmpty();
    }
}