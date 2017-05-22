using System;
using System.Linq.Expressions;
using ToolBox.Utils;

namespace ToolBox.Expressions
{
    public class NotFilterExpression<T> : CompositeFilterExpression<T> where T : class
    {
        private readonly CompositeFilterExpression<T> other;

        public NotFilterExpression(CompositeFilterExpression<T> other)
        {
            other.Required();

            this.other = other;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            var leftExpr = other.AsExpression();

            return Expression.Lambda<Func<T, bool>>(Expression.Not(leftExpr.Body), leftExpr.Parameters[0]);
        }

    }
}
