using System.Collections.Generic;

namespace ExpectedObjects.Specs.TestTypes
{
    abstract class IndexOverrideBaseType<T>
    {
        protected readonly IList<T> _items;

        public IndexOverrideBaseType(IList<T> items)
        {
            _items = items;
        }

        public T this[int index] => _items[index];

        public int Count => _items.Count;

    }

    class IndexOverrideTypeBase<T> : IndexOverrideBaseType<T>
    {
        public IndexOverrideTypeBase(IList<T> items) : base(items)
        {
        }

        public T this[string index] => _items[int.Parse(index)];
    }

    class IndexOverrideType<T> : IndexOverrideTypeBase<T>
    {
        public IndexOverrideType(IList<T> items) : base(items)
        {
        }
    }
}