using System;
using ExpectedObjects.Specs.Extensions;
using ExpectedObjects.Specs.Properties;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class IgnoreSpecs
    {
        [Subject("Ignoring Equals Override")]
        public class when_comparing_unequal_record_types_for_equality_with_equals_override_ignored
        {
            static Person _actual;
            static ExpectedObject _expected;

            static Exception _exception;

            Establish context = () =>
            {
                _expected = new Person {FirstName = "firstName1", LastName = "lastName"}
                    .ToExpectedObject(ctx => ctx.IgnoreEqualsOverride());

                _actual = new Person {FirstName = "firstName2", LastName = "lastName"};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();

            It should_report_differences_for_differing_members_only = () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_013);
        }

        [Subject("Ignoring Equals Override")]
        public class when_comparing_unequal_record_types_for_equality_configured_for_value_based_equality
        {
            static Person _actual;
            static ExpectedObject _expected;

            static Exception _exception;

            Establish context = () =>
            {
                _expected = new Person {FirstName = "firstName1", LastName = "lastName"}
                    .ToExpectedObject(ctx => ctx.CompareByValue<Person>());

                _actual = new Person {FirstName = "firstName2", LastName = "lastName"};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();

            It should_report_differences_for_differing_members_only = () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_013);
        }
    }
}