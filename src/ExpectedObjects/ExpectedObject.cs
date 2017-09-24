using System;
using ExpectedObjects.Reporting;

namespace ExpectedObjects
{
    public class ExpectedObject
    {
        readonly IConfiguration _configuration;
        readonly object _expected;

        public ExpectedObject(object instance, IConfiguration configuration)
        {
            _expected = instance;
            _configuration = configuration;
        }

        public override bool Equals(object actual)
        {
            var results = Equals(actual, new NullWriter());
            return results;
        }

        public bool Equals(object actual, IWriter writer, bool ignoreTypeInformation = false)
        {
            return new EqualityComparer(_configuration).AreEqual(_expected, actual, writer, ignoreTypeInformation);
        }

        public bool Equals(ExpectedObject other)
        {
            throw new ComparisonException($"Objects of type {nameof(ExpectedObject)} can not be compared to another instance of {nameof(ExpectedObject)}.");
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_expected != null ? _expected.GetHashCode() : 0) * 397) ^
                       (_configuration != null ? _configuration.GetHashCode() : 0);
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
    }
}