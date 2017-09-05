namespace ExpectedObjects
{
	public interface IComparison
	{
		bool AreEqual(object actual);
		object GetExpectedResult();
	}
}