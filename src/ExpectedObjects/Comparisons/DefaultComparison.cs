namespace ExpectedObjects.Comparisons
{
    public class DefaultComparison<T> : IComparison
    {
        public bool AreEqual(object actual)
        {
            return actual is T && ((T) actual).Equals(default(T));
        }

        public object GetExpectedResult()
        {
            return default(T).ToObjectString();
        }
    }
}