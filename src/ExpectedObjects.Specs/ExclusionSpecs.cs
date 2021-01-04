using System;
using System.Collections.Generic;
using ExpectedObjects.Specs.Properties;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class ExclusionSpecs
    {
        [Subject("Ignore Expression")]
        public class when_evaluating_equality_with_ignored_member_expression
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "test"}
                }.ToExpectedObject(ctx => ctx.Ignore(x => x.TypeWithString.StringProperty));

                _actual = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "different"}
                };
            };

            Because of = () => _results = _expected.Equals(_actual);

            It should_ignore_differences_in_property = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore Absolute")]
        public class when_evaluating_equality_with_ignored_absolute_member_path
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "ignore expected"}
                }.ToExpectedObject(ctx => ctx.IgnoreAbsolutePath("ComplexType.TypeWithString.StringProperty"));

                _actual = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "ignore actual"}
                };
            };

            Because of = () => _results = _expected.Equals(_actual);

            It should_ignore_differences_in_property = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore Absolute")]
        public class when_evaluating_equality_with_ignored_absolute_member_path2
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new ComplexType
                {
                    StringProperty = "ignore expected",
                    TypeWithString = new TypeWithString {StringProperty = "match"}
                }.ToExpectedObject(ctx => ctx.IgnoreAbsolutePath("ComplexType.StringProperty"));

                _actual = new ComplexType
                {
                    StringProperty = "ignore actual",
                    TypeWithString = new TypeWithString {StringProperty = "match"}
                };
            };

            Because of = () => _results = _expected.Equals(_actual);

            It should_ignore_differences_in_property = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore Relative")]
        public class when_evaluating_equality_with_ignored_relative_member_path
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "test"}
                }.ToExpectedObject(ctx => ctx.IgnoreRelativePath("TypeWithString.StringProperty"));

                _actual = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "different"}
                };
            };

            Because of = () => _results = _expected.Equals(_actual);

            It should_ignore_differences_in_property = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore Relative")]
        public class when_evaluating_equality_with_anonymous_ignored_relative_member_path
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new 
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "test"}
                }.ToExpectedObject(ctx => ctx.IgnoreRelativePath("TypeWithString.StringProperty"));

                _actual = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "different"}
                };
            };

            Because of = () => _results = _expected.Matches(_actual);

            It should_ignore_differences_in_property = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore Relative")]
        public class when_evaluating_equality_with_anonymous_ignored_member_expression
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new 
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "ignore expected"}
                }.ToExpectedObject(ctx => ctx.Ignore(x => x.TypeWithString.StringProperty));

                _actual = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "ignore actual"}
                };
            };

            Because of = () => _results = _expected.Matches(_actual);

            It should_ignore_differences_in_property = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore Relative")]
        public class when_evaluating_equality_with_equal_anonymous_ignored_ambiguous_member_expression
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new 
                {
                    StringProperty = "ignore expected",
                    TypeWithString = new TypeWithString {StringProperty = "level 2"}
                }.ToExpectedObject(ctx => ctx.Ignore(x => x.StringProperty));

                _actual = new ComplexType
                {
                    StringProperty = "ignore actual",
                    TypeWithString = new TypeWithString {StringProperty = "level 2"}
                };
            };

            Because of = () => _results = _expected.Matches(_actual);

            It should_ignore_differences_in_property = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore Relative")]
        public class when_evaluating_match_for_non_matching_types_with_ignored_member_matching_multiple_member_names
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new 
                {
                    StringProperty = "ignore expected",
                    TypeWithString = new TypeWithString {StringProperty = "x"}
                }.ToExpectedObject(ctx => ctx.Ignore(x => x.StringProperty));

                _actual = new ComplexType
                {
                    StringProperty = "ignore actual",
                    TypeWithString = new TypeWithString {StringProperty = "y"}
                };
            };

            Because of = () => _results = _expected.Matches(_actual);

            It should_recognize_differences_in_ignored_property = () => _results.ShouldBeFalse();
        }

        [Subject("Ignore Expression")]
        public class when_evaluating_match_with_ignored_enumerable_member_expression
        {
            static ExpectedObject _expected;
            static TypeWithElementList _actual;
            static bool _results;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithElementList
                {
                    Elements = new List<Element>
                    {
                        new Element
                        {
                            Data = "value"
                        }
                    }
                }.ToExpectedObject(ctx => ctx.Ignore(x => x.Elements[0].Id));

                _actual = new TypeWithElementList
                {
                    Id = 1,
                    Elements = new List<Element>
                    {
                        new Element
                        {
                            Id = 1,
                            Data = "value"
                        }
                    }
                };
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_not_ignore_the_same_named_property_on_containing_element = () => _exception.ShouldNotBeNull();
        }

        [Subject("Ignore Relative")]
        public class when_evaluating_match_with_ignored_member_relative_path
        {
            static ExpectedObject _expected;
            static TypeWithElementList _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithElementList
                {
                    Id = 1,
                    Elements = new List<Element>
                    {
                        new Element
                        {
                            Id = 1,
                            Data = "value"
                        }
                    }
                }.ToExpectedObject(ctx => ctx.IgnoreRelativePath("Id"));

                _actual = new TypeWithElementList
                {
                    Id = 2,
                    Elements = new List<Element>
                    {
                        new Element
                        {
                            Id = 2,
                            Data = "value"
                        }
                    }
                };
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_ignore_matching_properties = () => _exception.ShouldBeNull();
        }

        [Subject("Ignore Absolute")]
        public class when_evaluating_match_with_ignored_member_absolute_path
        {
            static ExpectedObject _expected;
            static TypeWithElementList _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithElementList
                {
                    Id = 1,
                    Elements = new List<Element>
                    {
                        new Element
                        {
                            Id = 1,
                            Data = "value"
                        }
                    }
                }.ToExpectedObject(ctx => ctx.IgnoreAbsolutePath("TypeWithElementList.Id"));

                _actual = new TypeWithElementList
                {
                    Id = 2,
                    Elements = new List<Element>
                    {
                        new Element
                        {
                            Id = 2,
                            Data = "value"
                        }
                    }
                };
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldMatch(_actual));

            It should_ignore_matching_properties = () => _exception.ShouldBeOfExactType<ComparisonException>();

            It should_have_the_expected_exception_message = () =>
                _exception.Message.ShouldEqual(Resources.when_evaluating_match_with_ignored_member_absolute_path);
        }

        [Subject("Ignore Relative")]
        public class when_evaluating_equality_with_ignored_member_relative_path
        {
            static ExpectedObject _expected;
            static TypeWithElementList _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithElementList
                {
                    Id = 1,
                    Elements = new List<Element>
                    {
                        new Element
                        {
                            Id = 1,
                            Data = "value"
                        }
                    }
                }.ToExpectedObject(ctx => ctx.IgnoreRelativePath("Id"));

                _actual = new TypeWithElementList
                {
                    Id = 2,
                    Elements = new List<Element>
                    {
                        new Element
                        {
                            Id = 2,
                            Data = "value"
                        }
                    }
                };
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));

            It should_ignore_matching_properties = () => _exception.ShouldBeNull();
        }

        [Subject("Ignore Expression")]
        public class when_evaluating_match_with_anonymous_type_ignored_member_expression
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "test"}
                }.ToExpectedObject(ctx => ctx.Ignore(x => x.TypeWithString.StringProperty));

                _actual = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "different"}
                };
            };

            Because of = () => _results = _expected.Matches(_actual);

            It should_ignore_differences_in_property = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore Expression")]
        public class when_evaluating_equality_with_ignored_root_type
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "test"}
                }.ToExpectedObject(ctx => ctx.Ignore(x => x));

                _actual = new ComplexType
                {
                    StringProperty = "level 2",
                    TypeWithString = new TypeWithString {StringProperty = "different"}
                };
            };

            Because of = () => _results = _expected.Equals(_actual);

            It should_ignore_differences = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore Absolute")]
        public class when_evaluating_equality_with_ignored_absolute_path_to_root_type
        {
            static ExpectedObject _expected;
            static ComplexType _actual;
            static bool _results;

            Establish context = () =>
            {
                _expected = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "test"}
                }.ToExpectedObject(ctx => ctx.IgnoreAbsolutePath("ComplexType"));

                _actual = new ComplexType
                {
                    StringProperty = "level 2",
                    TypeWithString = new TypeWithString {StringProperty = "different"}
                };
            };

            Because of = () => _results = _expected.Equals(_actual);

            It should_ignore_differences = () => _results.ShouldBeTrue();
        }
    }
}