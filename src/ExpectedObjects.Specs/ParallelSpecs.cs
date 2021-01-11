using System.Threading.Tasks;
using ExpectedObjects.Reporting;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class ParallelSpecs
    {
        [Subject("Parallel Tests")]
        class when_tests_are_run_in_parallel
        {
            static ExpectedObject _expectedObject1;
            static ExpectedObject _expectedObject2;
            static ShouldWriter _writer1;
            static ShouldWriter _writer2;

            Establish context = () =>
            {
                _expectedObject1 = 1.ToExpectedObject();
                _expectedObject2 = 2.ToExpectedObject();

                _writer1 = new ShouldWriter();
                _writer2 = new ShouldWriter();

                var task1 = new Task(() =>
                {
                    for (var i = 0; i < 1000; i++)
                        _expectedObject1.Equals(1, _writer1, false);
                });

                var task2 = new Task(() =>
                {
                    for (var i = 0; i < 1000; i++)
                        _expectedObject2.Equals(1, _writer2, false);
                });

                task1.Start();
                task2.Start();

                Task.WaitAll(task1, task2);
            };

            It should_isolate_test_reporting_output = () => _writer1.GetFormattedResults().ShouldBeEmpty();
        }
    }
}