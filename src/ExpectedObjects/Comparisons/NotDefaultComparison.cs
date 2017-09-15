using ExpectedObjects;

namespace ExpectedObjects.Comparisons
{
    public class NotDefaultComparison<T> : IComparison
    {
        public bool AreEqual(object actual)
        {
            return actual is T && !actual.Equals(default(T));
        }

        public object GetExpectedResult()
        {
            return $"value other than {ObjectStringExtensions.ToObjectString(default(T))}";
        }
    }
}