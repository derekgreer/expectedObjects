namespace ExpectedObjects.Specs.TestTypes
{
    abstract class BaseType
    {
        public virtual long Value { get; set; }
        public virtual int OverriddenValue { get; set; }
        public virtual string InheritedValue { get; set; }
    }

    class HidingTypeBase : BaseType
    {
        public int NonHiddenValue { get; set; }
        public new int Value { get; set; }
        public override int OverriddenValue { get; set; }
        public virtual string AddedValue { get; set; }
    }

    class HidingType : HidingTypeBase
    {
    }
}