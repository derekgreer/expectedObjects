using System;
using ExpectedObjects.Specs.Extensions;
using ExpectedObjects.Specs.Properties;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class DifferentTypesSpecs
    {
        [Subject("Different Types")]
        class when_comparing_different_types_with_equal_members_for_match_after_ignored_expression
        {
            static MessageV2 _actual;
            static Exception _exception;
            static ExpectedObject _expected;

            Establish context = () =>
            {
                _expected = new MessageV1 {OrderId = 42, DeprecatedInfo = "ignore"}.ToExpectedObject(ctx => ctx.Ignore(x => x.DeprecatedInfo));
                _actual = new MessageV2 { OrderId = 42, AdditionalOrderInformation = "not compared"};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_not_throw_exception = () => _exception.ShouldBeNull();
        }

        [Subject("Different Types")]
        class when_comparing_different_types_with_equal_members_for_match_after_ignored_absolute_path
        {
            static MessageV2 _actual;
            static Exception _exception;
            static ExpectedObject _expected;

            Establish context = () =>
            {
                _expected = new MessageV1 {OrderId = 42, DeprecatedInfo = "ignore"}.ToExpectedObject(ctx => ctx.IgnoreAbsolutePath("MessageV1.DeprecatedInfo"));
                _actual = new MessageV2 { OrderId = 42, AdditionalOrderInformation = "not compared"};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_not_throw_exception = () => _exception.ShouldBeNull();
        }

        [Subject("Different Types")]
        class when_comparing_different_types_with_equal_members_for_match_after_ignored_relative_path
        {
            static MessageV2 _actual;
            static Exception _exception;
            static ExpectedObject _expected;

            Establish context = () =>
            {
                _expected = new MessageV1 {OrderId = 42, DeprecatedInfo = "ignore"}.ToExpectedObject(ctx => ctx.IgnoreRelativePath("DeprecatedInfo"));
                _actual = new MessageV2 { OrderId = 42, AdditionalOrderInformation = "not compared"};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_not_throw_exception = () => _exception.ShouldBeNull();
        }

        [Subject("Different Types")]
        class when_comparing_different_types_with_equal_members_for_match
        {
            static TypeWithString2 _actual;
            static Exception _exception;
            static ExpectedObject _expected;

            Establish context = () =>
            {
                _expected = new TypeWithString {StringProperty = "this is a test"}.ToExpectedObject();
                _actual = new TypeWithString2 {StringProperty = "this is a test"};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_not_throw_exception = () => _exception.ShouldBeNull();
        }

        [Subject("Different Types")]
        class when_comparing_different_types_with_equal_members_for_match_with_ignore
        {
            static ComplexType2 _actual;
            static Exception _exception;
            static ExpectedObject _expected;

            Establish context = () =>
            {
                _expected = new ComplexType {StringProperty = "this is a test"}.ToExpectedObject(ctx => ctx.Ignore(x => x.IntegerProperty));
                _actual = new ComplexType2 {StringProperty = "this is a test", IntegerProperty = 42};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_not_throw_exception = () => _exception.ShouldBeNull();
        }

        [Subject("Different Types")]
        class when_comparing_different_types_with_equal_members_for_equality_with_ignore
        {
            static ComplexType2 _actual;
            static Exception _exception;
            static ExpectedObject _expected;

            Establish context = () =>
            {
                _expected = new ComplexType {StringProperty = "this is a test"}.ToExpectedObject(ctx => ctx.Ignore(x => x.IntegerProperty));
                _actual = new ComplexType2 {StringProperty = "this is a test", IntegerProperty = 42};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));

            It should_not_throw_exception = () => _exception.ShouldBeOfExactType<ComparisonException>();

            It should_report_the_expected_exception = () => _exception.Message.ShouldEqual(Resources.ExceptionMessage_015);
        }

        [Subject("Different Types")]
        class when_comparing_equal_structurally_equivalent_types_for_match
        {
            static ComplexType3 _actual;
            static Exception _exception;
            static ExpectedObject _expected;

            Establish context = () =>
            {
                _expected = new ComplexType {TypeWithString = new TypeWithString {StringProperty = "this is a test"}}
                    .ToExpectedObject(ctx => ctx.Ignore(x => x.IntegerProperty));
                _actual = new ComplexType3 {TypeWithString = new TypeWithString2 {StringProperty = "this is a test"}};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_not_throw_exception = () => _exception.ShouldBeNull();
        }

        [Subject("Different Types")]
        class when_comparing_different_types_with_equality_for_match_with_ignore
        {
            static ComplexType2 _actual;
            static Exception _exception;
            static ExpectedObject _expected;

            Establish context = () =>
            {
                _expected = new ComplexType {StringProperty = "this is a test"}.ToExpectedObject(ctx => ctx.Ignore(x => x.IntegerProperty));
                _actual = new ComplexType2 {StringProperty = "this is a test", IntegerProperty = 42};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));

            It should_not_throw_exception = () => _exception.ShouldNotBeNull();
        }
    }
}