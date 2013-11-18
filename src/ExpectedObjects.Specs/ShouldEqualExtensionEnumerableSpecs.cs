using System;
using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    public class when_asserting_equality_for_unequal_objects_with_enumerable_of_simple
    {
        static TypeWithIEnumerable<SimpleType> _actual;
        static Exception _exception;
        static TypeWithIEnumerable<SimpleType> _expected;

        Establish context = () =>
            {
                _expected = new TypeWithIEnumerable<SimpleType>
                                {
                                    Objects = new List<SimpleType>
                                                  {
                                                      new SimpleType {IntProperty = 1, StringProperty = "test1"}
                                                  }
                                };

                _actual = new TypeWithIEnumerable<SimpleType>
                              {
                                  Objects = new List<SimpleType>
                                                {
                                                    new SimpleType {IntProperty = 1, StringProperty = "test2"}
                                                }
                              };


                var ex = new Exception();
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual(_actual));

        It should_throw_exception_with_subscripted_values =
            () =>
            _exception.Message.ShouldEqual(
                string.Format(
                    "For TypeWithIEnumerable`1.Objects[0].StringProperty, expected \"test1\" but found \"test2\".{0}",
                    Environment.NewLine));
    }

    public class when_asserting_equality_for_unequal_objects_with_enumerable_of_different_count
    {
        static TypeWithIEnumerable _actual;
        static Exception _exception;
        static TypeWithIEnumerable _expected;

        Establish context = () =>
            {
                _expected = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2"}};
                _actual = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2", "test3"}};
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual(_actual));

        It should_throw_exception_with_subscripted_values =
            () =>
            _exception.Message.ShouldEqual(
                string.Format("For TypeWithIEnumerable.Objects[2], expected nothing but found \"test3\".{0}",
                              Environment.NewLine));
    }

    public class when_asserting_equality_for_enumerables_with_different_values
    {
        static TypeWithIEnumerable _actual;
        static Exception _exception;
        static TypeWithIEnumerable _expected;

        Establish context = () =>
            {
                _expected = new TypeWithIEnumerable {Objects = new List<string> {"test1", "test2"}};
                _actual = new TypeWithIEnumerable {Objects = new List<string> {"test3", "test4"}};
            };

        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual(_actual));

        It should_throw_exception_with_subscripted_values =
            () =>
            _exception.Message.ShouldEqual(
                string.Format("For TypeWithIEnumerable.Objects[0], expected \"test1\" but found \"test3\".{0}" +
                              "For TypeWithIEnumerable.Objects[1], expected \"test2\" but found \"test4\".{0}",
                              Environment.NewLine));
    }

	public class when_asserting_equality_for_enumerables_with_fewer_elements_with_ignore_types
	{
		static List<string> _actual;
		static Exception _exception;
		static List<string> _expected;

		Establish context = () =>
		{
			_expected = new List<string> { "test1", "test2", "test3" };
			_actual = new List<string> { "test1", "test2" };
		};


		Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().IgnoreTypes().ShouldEqual(_actual));

		It should_throw_exception_with_subscripted_values =
			() =>
			_exception.Message.ShouldEqual(
				string.Format("For List`1[2], expected \"test3\" but element was missing.{0}", Environment.NewLine));
	}

    public class when_asserting_equality_for_enumerables_with_fewer_elements
    {
        static List<string> _actual;
        static Exception _exception;
        static List<string> _expected;

        Establish context = () =>
            {
                _expected = new List<string> {"test1", "test2", "test3"};
                _actual = new List<string> {"test1", "test2"};
            };


        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual(_actual));

        It should_throw_exception_with_subscripted_values =
            () =>
            _exception.Message.ShouldEqual(
                string.Format("For List`1[2], expected \"test3\" but element was missing.{0}", Environment.NewLine));
    }

    public class when_asserting_equality_for_enumerables_with_more_elements
    {
        static List<string> _actual;
        static Exception _exception;
        static List<string> _expected;

        Establish context = () =>
            {
                _expected = new List<string> {"test1", "test2"};
                _actual = new List<string> {"test1", "test2", "test3"};
            };


        Because of = () => _exception = Catch.Exception(() => _expected.ToExpectedObject().ShouldEqual(_actual));

        It should_throw_exception_with_subscripted_values = () =>
            _exception.Message.ShouldEqual(string.Format("For List`1[2], expected nothing but found \"test3\".{0}", Environment.NewLine));
    }
}