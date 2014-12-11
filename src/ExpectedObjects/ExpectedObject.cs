using System;

namespace ExpectedObjects
{
    public class ExpectedObject
    {
        readonly IConfigurationContext _configurationContext = new ConfigurationContext();
        readonly object _expected;

        public ExpectedObject(object expected)
        {
            _expected = expected;
        }

        public override bool Equals(object actual)
        {
            return new EqualityComparer((IConfiguredContext)_configurationContext).AreEqual(_expected, actual);
        }

        public bool Equals(ExpectedObject other)
        {
            throw new Exception("ExpectedObject is not intended to be compared to another ExpectedObject");
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_expected != null ? _expected.GetHashCode() : 0)*397) ^
                       (_configurationContext != null ? _configurationContext.GetHashCode() : 0);
            }
        }

        public static bool operator ==(ExpectedObject left, ExpectedObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ExpectedObject left, ExpectedObject right)
        {
            return !Equals(left, right);
        }

        public ExpectedObject Configure(Action<IConfigurationContext> configuration)
        {
            configuration(_configurationContext);
            return this;
        }
    }
}