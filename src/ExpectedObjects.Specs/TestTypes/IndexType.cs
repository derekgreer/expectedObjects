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

        public T this[int index]
        {
            get { return _ints[index]; }
        }

        public int Count
        {
            get { return _ints.Count; }
        }
    }
}