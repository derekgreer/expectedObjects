namespace ExpectedObjects
{
	public interface IComparison
	{
		bool AreEqual(object o);
		object GetExpectedResult();
	}
}