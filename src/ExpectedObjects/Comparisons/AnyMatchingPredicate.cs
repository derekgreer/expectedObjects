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

        public bool AreEqual(object actual)
        {
            var predicate = _predicate.Compile();
            return actual is T && predicate((T) actual);
        }

        public object GetExpectedResult()
        {
            return string.Format("any instance of {0} where {1}", typeof(T).ToUsefulTypeName(), _predicate.Body.ToString());
        }
    }
}