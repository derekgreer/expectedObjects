using System.Collections.Generic;
using System.Linq;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{
    public class when_comparing_unequal_object_with_string_configured_with_writer
    {
        static TypeWithString _actual;
        static TypeWithString _expected;
        static ExpectedObjects.ExpectedObject _expectedObject;
        static Mock<IWriter> _mockWriter;
        static List<EqualityResult> _results = new List<EqualityResult>();

        Establish context = () =>
            {
                _mockWriter = new Mock<IWriter>();
                _mockWriter
                    .Setup(x => x.Write(Moq.It.IsAny<EqualityResult>()))
                    .Callback<EqualityResult>(result => _results.Add(result));

                _expected = new TypeWithString {StringProperty = "test"};
                _expectedObject = new ExpectedObjects.ExpectedObject(_expected).Configure(ctx =>
                    {
                        ctx.PushStrategy<ComparableComparisonStrategy>();
                        ctx.PushStrategy<ClassComparisonStrategy>();
                        ctx.SetWriter(_mockWriter.Object);
                    });

                _actual = new TypeWithString {StringProperty = "error"};
            };

        Because of = () => _expectedObject.Equals(_actual);

        It should_write_errors_to_the_writer =
            () => _mockWriter.Verify(x => x.Write(Moq.It.IsAny<EqualityResult>()), Times.AtLeastOnce());

        It should_write_string_compare_expected_result_to_the_writer =
            () => ShouldExtensionMethods.ShouldNotBeNull(_results.Select(x => x.Member.Equals("StringProperty")));
    }

    public class when_comparing_equal_objects_with_string
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

    public class when_comparing_unequal_objects_with_string_field_configured_for_fields
    {
        static TypeWithStringField _actual;
        static ExpectedObjects.ExpectedObject _expected;
        static bool _result;

        Establish context = () =>
            {
                _expected = new TypeWithStringField {StringField = "test"}
                    .ToExpectedObject()
                    .Configure(ctx => ctx.Include(MemberType.PublicFields));
                _actual = new TypeWithStringField {StringField = "test2"};
            };

        Because of = () => _result = _expected.Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    public class when_comparing_unequal_objects_with_string_field_not_configured_for_fields
    {
        static TypeWithStringField _actual;
        static ExpectedObjects.ExpectedObject _expected;
        static bool _result;

        Establish context = () =>
            {
                _expected = new TypeWithStringField {StringField = "test"}.ToExpectedObject();
                _actual = new TypeWithStringField { StringField = "test2" };
            };

        Because of = () => _result = _expected.Equals(_actual);

        It should_not_be_equal = () => _result.ShouldBeFalse();
    }

    public class when_comparing_unequal_objects_with_string
    {
        static TypeWithString _actual;
        static TypeWithString _expected;

        static bool _result;

        Establish context = () =>
            {
                _actual = new TypeWithString { StringProperty = "test" };
                _expected = new TypeWithString { StringProperty = "test2" };
            };

        Because of = () => _result = _expected.ToExpectedObject().Equals(_actual);

        It should_be_equal = () => _result.ShouldBeFalse();
    }
}