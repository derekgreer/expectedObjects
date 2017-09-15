namespace ExpectedObjects.Specs.TestTypes
{
    public class ComplexType
    {
        public int IntegerProperty { get; set; }
        public string StringProperty { get; set; }
        public decimal DecimalProperty { get; set; }

        public TypeWithString TypeWithString { get; set; }
        public TypeWithIEnumerable TypeWithIEnumerable { get; set; }
        public IndexType<int> IndexType { get; set; }
    }
}