namespace ExpectedObjects
{
	public class ComparisonResult : IComparisionResult
	{
		readonly object _actual;

		public ComparisonResult(object actual, bool result)
		{
			Result = result;
			_actual = actual;
		}

		public bool Result { get; set; }

		public bool Equals(ComparisonResult other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other._actual, _actual);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (ComparisonResult)) return false;
			return Equals((ComparisonResult) obj);
		}

		public override int GetHashCode()
		{
			return (_actual != null ? _actual.GetHashCode() : 0);
		}
	}
}