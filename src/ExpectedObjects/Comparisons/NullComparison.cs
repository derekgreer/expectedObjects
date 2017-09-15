namespace ExpectedObjects.Comparisons
{
    public class NullComparison : IComparison
    {
        public bool AreEqual(object actual)
        {
            return actual == null;
        }

        public object GetExpectedResult()
        {
            return "a null value";
        }
    }
}