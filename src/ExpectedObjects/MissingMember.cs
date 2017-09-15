using System;

namespace ExpectedObjects
{
    public class MissingMember<T> : IMissingMember
    {
        public Type MemberType => typeof(T);
    }
}