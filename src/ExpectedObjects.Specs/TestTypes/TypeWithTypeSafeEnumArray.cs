namespace ExpectedObjects.Specs.TestTypes
{
    public class TypeWithTypeSafeEnumArray
    {
        public int Id { get; set; }
        public TypeWithTypeSafeEnumField[] Enums { get; set; }
    }

    public class TypeWithTypeSafeEnumField
    {
        public int Id { get; set; }
        public TypeSafeEnumType TypeSafeTypeSafeEnumProperty => TypeSafeEnumType.One;
    }

    public class TypeSafeEnumType
    {
        public static readonly TypeSafeEnumType One = new() {Id = 1, Name = "One"};
        public static readonly TypeSafeEnumType Two = new() {Id = 2, Name = "Two"};
        public static readonly TypeSafeEnumType Three = new() {Id = 3, Name = "Three"};
        public int Id { get; set; }
        public string Name { get; set; }
    }
}