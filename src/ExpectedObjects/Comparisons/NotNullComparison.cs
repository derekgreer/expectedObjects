namespace ExpectedObjects.Comparisons
{
	public class NotNullComparison : IComparison
	{
		public bool AreEqual(object actual)
		{
			return actual != null;
		}

		public object GetExpectedResult()
		{
			return "a non-null value";
		}
	}
}