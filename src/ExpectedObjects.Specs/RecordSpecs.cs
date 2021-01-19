using System;
using ExpectedObjects.Specs.Properties;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class RecordSpecs
    {
        [Subject("Record Comparisons")]
        public class when_comparing_equal_record_types_for_equality
        {
            static Person _actual;
            static ExpectedObject _expected;

            Establish context = () =>
            {
                _expected = new Person {FirstName = "firstName", LastName = "lastName"}.ToExpectedObject();

                _actual = new Person {FirstName = "firstName", LastName = "lastName"};
            };

            It should_be_equal = () => _expected.ShouldEqual(_actual);
        }

        [Subject("Record Comparisons")]
        public class when_comparing_unequal_record_types_for_equality
        {
            static Person _actual;
            static ExpectedObject _expected;

            static Exception _exception;

            Establish context = () =>
            {
                _expected = new Person {FirstName = "firstName1", LastName = "lastName"}.ToExpectedObject();

                _actual = new Person {FirstName = "firstName2", LastName = "lastName"};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();

            It should_report_differences_including_the_entire_object = () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_012);
        }

        [Subject("Record Comparisons")]
        public class when_comparing_equal_type_with_record_property_for_equality
        {
            static TypeWithRecordProperty _actual;
            static ExpectedObject _expected;

            Establish context = () =>
            {
                _expected = new TypeWithRecordProperty
                {
                    Person = new Person {FirstName = "firstName", LastName = "lastName"},
                    Scopes = new[] {"do:something", "do:something:else"}
                }.ToExpectedObject();

                _actual = new TypeWithRecordProperty
                {
                    Person = new Person {FirstName = "firstName", LastName = "lastName"},
                    Scopes = new[] {"do:something", "do:something:else"}
                };
            };

            It should_be_equal = () => _expected.ShouldEqual(_actual);
        }
    }
}