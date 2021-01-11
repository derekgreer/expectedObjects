using System;
using System.Xml.Linq;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;

namespace ExpectedObjects.Specs
{
    class XElementSpecs
    {
        [Subject("XElement Type")]
        class when_comparing_equal_types_with_an_xelement
        {
            static ExpectedObject _expected;
            static TypeWithXElement _actual;
            static Exception _exception;

            Establish context = () =>
            {
                _expected = new TypeWithXElement {XElementProperty = XElement.Parse("<root></root>")}.ToExpectedObject();
                _actual = new TypeWithXElement {XElementProperty = XElement.Parse("<root></root>")};
            };

            Because of = () => _exception = Catch.Exception(() => _expected.ShouldEqual(_actual));

            It should_be_equal = () => _exception.ShouldBeNull();
        }
    }
}