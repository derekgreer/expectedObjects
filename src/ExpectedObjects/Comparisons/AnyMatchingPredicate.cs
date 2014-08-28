using System;
using System.Linq.Expressions;

namespace ExpectedObjects.Comparisons
{
	public class AnyMatchingPredicate<T> : IComparison
	{
		readonly Expression<Func<T, bool>> _predicate;
		
		public AnyMatchingPredicate(Expression<Func<T, bool>> predicate)
		{
			_predicate = predicate;
		}

		public bool AreEqual(object o)
		{
			var predicate = _predicate.Compile();
			return (o is T) && predicate((T)o);
		}

		public object GetExpectedResult()
		{
			return string.Format("any instance of {0} where {1}", typeof(T).FullName, _predicate.Body.ToString());
		}
	}
}