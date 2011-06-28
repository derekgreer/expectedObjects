using System.Collections;
using System.Collections.Generic;

namespace ExpectedObjects.Specs.TestTypes
{
    public class TypeWithIEnumerable
    {
        public IEnumerable Objects { get; set; }
    }

    public class TypeWithIEnumerable<T>
    {
        public IEnumerable<T> Objects { get; set; }
    }
}