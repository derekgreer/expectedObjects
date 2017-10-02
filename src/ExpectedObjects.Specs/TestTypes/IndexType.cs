using System.Collections.Generic;

namespace ExpectedObjects.Specs.TestTypes
{
    public class IndexType<T>
    {
        readonly IList<T> _values;

        public IndexType(IList<T> values)
        {
            _values = values;
        }

        public T this[int index] => _values[index];

        public int Count => _values.Count;
    }
}