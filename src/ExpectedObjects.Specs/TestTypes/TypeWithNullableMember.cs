using System;

namespace ExpectedObjects.Specs.TestTypes
{
    public class TypeWithNullableMember
    {
        public int TestInt { get; set; }
        public string TestString { get; set; }
        public DateTime TestDateTime { get; set; }
        public DateTime? TestNullDateTime { get; set; }
    }
}