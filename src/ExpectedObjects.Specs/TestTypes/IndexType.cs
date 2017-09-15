using System.Collections.Generic;

namespace ExpectedObjects.Specs.TestTypes
{
    public class IndexType<T>
    {
        readonly IList<T> _ints;

        public IndexType(IList<T> ints)
        {
            _ints = ints;
        }

        public T this[int index] => _ints[index];

        public int Count => _ints.Count;
    }
}