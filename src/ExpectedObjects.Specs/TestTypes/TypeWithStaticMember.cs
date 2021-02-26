namespace ExpectedObjects.Specs.TestTypes
{
    public class TypeWithStaticMember
    {
        public TypeWithStaticMember()
        {
            //StaticMember = new TypeWithString {StringProperty = StaticMember.StringProperty + "."};
        }

        public int IntProperty { get; set; }
        public static TypeWithString StaticProperty
        {
            get
            {
                return new() {StringProperty = "test"};
            }
        }
    }
}