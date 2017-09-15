using ExpectedObjects.Reporting;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    [Subject("Results Formatting")]
    public class when_retrieving_formatted_result_from_composite_comparison
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

    [Subject("Results Formatting")]
    public class when_retrieving_formatted_result_from_equal_comparison
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