using System;

namespace ExpectedObjects
{
    public class ComparisonException : Exception
    {
        public ComparisonException(string message) : base(message)
        {
        }
    }
}