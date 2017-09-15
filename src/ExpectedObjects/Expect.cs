using System;
using System.Linq.Expressions;
using ExpectedObjects.Comparisons;
using ExpectedObjects.Strategies;

namespace ExpectedObjects
{
    /// <summary>
    ///     Convenience class for configuring provided instances of <see cref="ExpectedObjects.IComparison" />.
    /// </summary>
    /// <remarks>
    ///     Types returned must be of the derived type to be recognized properly
    ///     by the <see cref="ExpectedObjects.EqualityComparer" />
    /// </remarks>
    public static class Expect
    {
        static readonly NotNullComparison NotNullComparison = new NotNullComparison();
        static readonly NullComparison NullComparison = new NullComparison();
        static readonly IgnoredComparison IgnoredComparison = new IgnoredComparison();

        public static Any<T> Any<T>()
        {
            return new Any<T>();
        }

        public static AnyMatchingPredicate<T> Any<T>(Expression<Func<T, bool>> predicateExpression)
        {
            return new AnyMatchingPredicate<T>(predicateExpression);
        }

        public static NotNullComparison NotNull()
        {
            return NotNullComparison;
        }

        public static NullComparison Null()
        {
            return NullComparison;
        }

        public static IComparison Ignored()
        {
            return IgnoredComparison;
        }

        public static IComparison Default<T>()
        {
            return new DefaultComparison<T>();
        }

        public static IComparison NotDefault<T>()
        {
            return new NotDefaultComparison<T>();
        }
    }
}