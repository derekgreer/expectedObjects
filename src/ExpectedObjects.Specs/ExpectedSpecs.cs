using System;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{

    public class when_expected_created_from_single_stub
    {
        private static ComplexType _testStub;
        private static TestDto _expected = new TestDto()
        {
            StringProperty = "aaaaaa",
            TypeWithIEnumerable = new TypeWithIEnumerable() { Objects = new[] { 1, 2, 3 } },
            TypeWithString = new TypeWithString() { StringProperty = "bbbb" }
        };
        private static ExpectedObject _actual;
        private static bool _result;
        private static Mock<IComparison> _comparisionSpy;

        Establish context = () =>
        {
            _actual = _expected.WithSameProperties<TestDto1>();
        };

        Because of = () => _result = _actual.Equals(_expected);

        It should_match_expected_with_actual = () => _result.ShouldBeTrue();
    }

    public class when_actual_has_unexpected_properties
    {
        private static ComplexType _testStub;
        private static TestDto _expected =  new TestDto()
            {
                StringProperty = "aaaaaa",
                TypeWithIEnumerable = new TypeWithIEnumerable(){Objects = new[]{1,2,3}},
                TypeWithString = new TypeWithString() { StringProperty = "bbbb"}  // no mention of this is expected- actual should default to not not comparison
            };
        private static ExpectedObject _actual;
        private static bool _result;
        private static Mock<IComparison> _comparisionSpy;

        Establish context = () =>
        {

            _actual = _expected.WithSelectedProperties<TestDto, TestDto>(a=>a.StringProperty,a=>a.TypeWithIEnumerable);
        };

        Because of = () => _result = _actual.Equals(_expected);

        It should_fill_remainding_properties_with_default_comparisons = () => _result.ShouldBeTrue();
    }

    public class when_actual_is_missing_property_and_autofill_properties_with_default_comparisons_disabled
    {
        private static ComplexType _testStub;
        private static TestDto _expected = new TestDto()
        {
            StringProperty = "aaaaaa",
            TypeWithIEnumerable = new TypeWithIEnumerable() { Objects = new[] { 1, 2, 3 } },
            //    TypeWithString = new TypeWithString() { StringProperty = "bbbb"}
        };
        private static ExpectedObject _actual;
        private static bool _result;
        private static Mock<IComparison> _comparisionSpy;

        Establish context = () =>
        {
            _actual = _expected.ToDto<TestDto, TestDto>(false,a=>a.StringProperty,a=>a.TypeWithIEnumerable);
            _exception = Catch.Exception(() => _actual.Equals(_expected));
        };

        Because of = () =>  _exception.ShouldNotBeNull();

        private It should_throw_error_with_missing_property_detail = () => _result.ShouldBeFalse();
        private static Exception _exception;
    }

    public class when_mapping_from_basic_stub_to_plain_object
    {
        private static ComplexType _testStub;
        private static ExpectedObject _expected;
        private static TestDto _actual;
        private static bool _result;
        private static Mock<IComparison> _comparisionSpy;

        private Establish context = () =>
        {
            _testStub = TestUtil.BuildComplexType();

            _expected = _testStub.WithSelectedProperties<ComplexType, object>(
                a => a.TypeWithIEnumerable,
                a => a.TypeWithString,
                a => a.StringProperty);

            _actual = new TestDto()
            {
                StringProperty = _testStub.StringProperty,
                TypeWithIEnumerable = _testStub.TypeWithIEnumerable,
                TypeWithString = _testStub.TypeWithString
            };
        };

        private Because of = () => _result = _expected.Equals(_actual);

        private It should_match_expected_with_actual = () => _result.ShouldBeTrue();
    }

    public class when_mapping_from_stub_with_anonymous_name_to_dto
    {
        private static ComplexType _testStub;
        private static ExpectedObject _expected;
        private static bool _result;
        private static Mock<IComparison> _comparisionSpy;

        private Establish context = () =>
        {

            _testStub = new Mock<ComplexType>().Object;
            _testStub.StringProperty = "aaaaaaaaaaa";
            _testStub.TypeWithString = new TypeWithString() { StringProperty = "xxxxxxx" };
            _testStub.TypeWithIEnumerable = new TypeWithIEnumerable();

            _expected = _testStub.WithSelectedProperties<ComplexType, TestDto2>(
                a => a.Map<ComplexType, TestDto2>(b => b.StringProperty, c => c.Name1),
                a => a.Map<ComplexType, TestDto2>(b => b.TypeWithString, c => c.Name2),
                a => a.Map<ComplexType, TestDto2>(b => b.TypeWithIEnumerable, c => c.Name3));

        };

        Because of = () => _result =

        _expected.Equals(new
        {
            Name1 = _testStub.StringProperty,
            Name2 = _testStub.TypeWithString,
            Name3 = _testStub.TypeWithIEnumerable
        });


        It should_match_expected_with_actual = () => _result.ShouldBeTrue();
    }


    public static class TestUtil
    {
        public static ComplexType BuildComplexType()
        {
            var testStub = new ComplexType
            {
                DecimalProperty = 2m,
                StringProperty = "abcd987",
                TypeWithIEnumerable = new TypeWithIEnumerable() {Objects = new[] {1, 2, 3, 45}},
                IntegerProperty = 9,
                TypeWithString = new TypeWithString() {StringProperty = "typewithstring.stringproperty"}
            };
            testStub.IntegerProperty = 999;
            return testStub;
        }
    }
}


