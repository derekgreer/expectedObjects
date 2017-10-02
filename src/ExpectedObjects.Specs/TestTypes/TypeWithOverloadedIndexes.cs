using System.Collections.Generic;

namespace ExpectedObjects.Specs.TestTypes
{
    public class TypeWithOverloadedIndexes<T>
    {
        readonly IList<T> _items;

        public TypeWithOverloadedIndexes(IList<T> items)
        {
            _items = items;
        }

        public T this[int index] => _items[index];
        public T this[string index] => _items[int.Parse(index)];

        public int Count => _items.Count;
    }
}