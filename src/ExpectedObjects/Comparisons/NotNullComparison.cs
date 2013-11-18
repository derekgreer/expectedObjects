namespace ExpectedObjects.Comparisons
{
	public class NotNullComparison : IComparison
	{
		public bool AreEqual(object o)
		{
			return o != null;
		}

		public object GetExpectedResult()
		{
			return "a non-null value";
		}
	}
}