using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{
    public class TestDto
    {
        public string StringProperty { get; set; }
        public TypeWithString TypeWithString { get; set; }
        public TypeWithIEnumerable TypeWithIEnumerable { get; set; }
    }

    public class TestDto2
    {
        public TestDto2()
        {
        }

        public string Name1 { get; set; }
        public TypeWithString Name2 { get; set; }
        public TypeWithIEnumerable Name3 { get; set; }
    }

    public class when_mapping_from_basic_stub_to_dto
    {
        private static ComplexType _testStub;
        private static ExpectedObject _expected;
        private static TestDto _actual;
        private static bool _result;
        private static Mock<IComparison> _comparisionSpy;

        private Establish context = () =>
        {
            _testStub = new Mock<ComplexType>().Object;

            _expected = _testStub.ToEoDto<ComplexType, TestDto>(
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

    public class when_mapping_from_stub_with_anon_name_to_dto
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

            _expected = _testStub.ToEoDto<ComplexType, TestDto2>(
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
    }


