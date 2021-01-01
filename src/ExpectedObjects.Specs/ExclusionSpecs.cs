using System;
using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class ExclusionSpecs
    {
        [Subject("Ignore")]
        public class when_excluding_a_property
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

        [Subject("Ignore")]
        public class when_excluding_a_property_by_string_path
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
                }.ToExpectedObject(ctx => ctx.Ignore("ComplexType.TypeWithString.StringProperty"));

                _actual = new ComplexType
                {
                    StringProperty = "level 1",
                    TypeWithString = new TypeWithString {StringProperty = "different"}
                };
            };

            Because of = () => _results = _expected.Equals(_actual);

            It should_ignore_differences_in_property = () => _results.ShouldBeTrue();
        }

        [Subject("Ignore")]
        public class when_excluding_a_list_property
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

        [Subject("Ignore")]
        public class when_excluding_all_properties_matching_path
        {
            static ExpectedObject _expected;
            static TypeWithElementList _actual;
            static bool _results;
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
                }.ToExpectedObject(ctx => ctx.Ignore("Id"));

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

        [Subject("Ignore")]
        public class when_excluding_a_property_with_anonymous_expected_objects
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

        [Subject("Ignore")]
        public class when_excluding_a_base_type
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
    }
}