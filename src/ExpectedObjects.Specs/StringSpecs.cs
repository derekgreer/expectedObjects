using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects.Reporting;
using ExpectedObjects.Specs.Extensions;
using ExpectedObjects.Specs.Properties;
using ExpectedObjects.Specs.TestTypes;
using ExpectedObjects.Strategies;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{
    class StringSpecs
    {
        [Subject("Writer")]
        class when_comparing_unequal_object_with_string_configured_with_writer
        {
            static TypeWithString _actual;
            static ExpectedObject _expected;

            static Mock<IWriter> _mockWriter;
            static readonly List<EqualityResult> _results = new();

            Establish context = () =>
            {
                _mockWriter = new Mock<IWriter>();
                _mockWriter
                    .Setup(x => x.Write(Moq.It.IsAny<EqualityResult>()))
                    .Callback<EqualityResult>(result => _results.Add(result));

                _expected = new TypeWithString {StringProperty = "test"}.ToExpectedObject(ctx =>
                {
                    ctx.PushStrategy<ComparableComparisonStrategy>();
                    ctx.PushStrategy<ValueComparisonStrategy>();
                });

                _actual = new TypeWithString {StringProperty = "error"};
            };

            Because of = () => _expected.Equals(_actual, _mockWriter.Object);

            It _should_write_string_compare_result_to_the_writer =
                () => _results.Select(x => x.Member.Equals("StringProperty")).ShouldNotBeNull();

            It should_write_errors_to_the_writer =
                () => _mockWriter.Verify(x => x.Write(Moq.It.IsAny<EqualityResult>()), Times.AtLeastOnce());
        }

        [Subject("Strings")]
        class when_comparing_equal_objects_with_string
        {
            static TypeWithString _actual;
            static TypeWithString _expected;

            static bool _result;

            Establish context = () =>
            {
                _actual = new TypeWithString {StringProperty = "test"};
                _expected = new TypeWithString {StringProperty = "test"};
            };

            Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

            It should_be_equal = () => _result.ShouldBeTrue();
        }

        [Subject("Strings")]
        class when_comparing_objects_with_unequeal_fields_not_configured_for_fields
        {
            static TypeWithStringMembers _actual;
            static ExpectedObject _expected;
            static bool _result;

            Establish context = () =>
            {
                _expected = new TypeWithStringMembers {StringProperty = "same", StringField = "test"}
                    .ToExpectedObject();
                _actual = new TypeWithStringMembers {StringProperty = "same", StringField = "test2"};
            };

            Because of = () => _result = _expected.Equals(_actual);

            It should_not_be_equal = () => _result.ShouldBeFalse();
        }

        [Subject("Strings")]
        class when_comparing_objects_with_unequal_string_fields_and_properties_not_configured_for_fields
        {
            static TypeWithStringMembers _actual;
            static TypeWithStringMembers _expected;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithStringMembers {StringProperty = "same", StringField = "test"};
                _actual = new TypeWithStringMembers {StringProperty = "different", StringField = "test2"};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldMatch(_actual));

            It should_report_differences_for_fields_and_properties = () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_004);

            It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();
        }

        [Subject("Strings")]
        class when_comparing_objects_with_unequal_string_properties
        {
            static TypeWithString _actual;
            static TypeWithString _expected;
            static Exception _exception;

            Establish context = () =>
            {
                _actual = new TypeWithString {StringProperty = "test"};
                _expected = new TypeWithString {StringProperty = "test2"};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldMatch(_actual));

            It should_report_differences_for_properties = () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_009);

            It should_throw_a_comparison_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();
        }
    }
}