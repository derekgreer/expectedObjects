namespace ExpectedObjects.Specs.TestTypes
{
    public class EqualsOverrideType
    {
        readonly bool _isEqual;

        public EqualsOverrideType(bool isEqual)
        {
            _isEqual = isEqual;
        }

        public override bool Equals(object obj)
        {
            return _isEqual;
        }

        public bool Equals(EqualsOverrideType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._isEqual.Equals(_isEqual);
        }

        public override int GetHashCode()
        {
            return _isEqual.GetHashCode();
        }

        public static bool operator ==(EqualsOverrideType left, EqualsOverrideType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EqualsOverrideType left, EqualsOverrideType right)
        {
            return !Equals(left, right);
        }
    }
}