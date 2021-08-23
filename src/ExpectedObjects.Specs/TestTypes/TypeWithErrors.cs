using System;
using System.Linq.Expressions;

namespace ExpectedObjects.Specs.TestTypes
{
    public class TypeWithExpression<TEntity>
    {
        public TypeWithExpression(Expression<Func<TEntity, object>> field)
        {
            PropertyExpression = field;
        }

        public Expression<Func<TEntity, object>> PropertyExpression { get; }
    }
}