namespace ExpectedObjects.Comparisons
{
	public class Any<T> : IComparison
	{
		public bool AreEqual(object o)
		{
			return (o is T);
		}

		public object GetExpectedResult()
		{
			return string.Format("any instance of {0}", typeof (T).FullName);
		}
	}
}