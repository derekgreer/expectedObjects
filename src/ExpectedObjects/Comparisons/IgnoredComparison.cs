namespace ExpectedObjects.Comparisons
{
    public class IgnoredComparison : IComparison
    {
        public bool AreEqual(object actual)
        {
            return true;
        }

        public object GetExpectedResult()
        {
            return null;
        }
    }
}