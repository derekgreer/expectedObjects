using System.Collections.Generic;

namespace ExpectedObjects
{
    public class StackDictionary<TKey, T>
    {
        readonly IDictionary<TKey, T> _dictionary = new Dictionary<TKey, T>();
        readonly Stack<T> _stack = new Stack<T>();

        public T this[TKey key] => _dictionary[key];

        public bool ContainsKey(TKey actual)
        {
            return _dictionary.ContainsKey(actual);
        }


        public void Push(TKey key, T item)
        {
            _stack.Push(item);
            _dictionary.Add(key, item);
        }

        public T Pop(TKey key)
        {
            _dictionary.Remove(key);
            return _stack.Pop();
        }
    }
}