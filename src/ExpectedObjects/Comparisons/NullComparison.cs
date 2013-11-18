namespace ExpectedObjects.Comparisons
{
	public class NullComparison : IComparison
	{
		public bool AreEqual(object o)
		{
			return o == null;
		}

		public object GetExpectedResult()
		{
			return "a null value";
		}
	}
}