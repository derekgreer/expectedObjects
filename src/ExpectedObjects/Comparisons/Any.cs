namespace ExpectedObjects.Comparisons
{
	public class Any<T> : IComparison
	{
		public bool AreEqual(object actual)
		{
			return (actual is T);
		}

		public object GetExpectedResult()
		{
			return string.Format("any instance of {0}", typeof (T).ToUsefulTypeName());
		}
	}
}