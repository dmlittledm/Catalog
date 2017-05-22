using System;
using System.Linq.Expressions;
using ToolBox.Utils;

namespace ToolBox.Expressions
{
    public class OrFilterExpression<T> : CompositeFilterExpression<T> where T : class
    {
        private readonly CompositeFilterExpression<T> left;
        private readonly CompositeFilterExpression<T> right;

        public OrFilterExpression(CompositeFilterExpression<T> left, CompositeFilterExpression<T> right)
        {
            left.Required();
            right.Required();

            this.left = left;
            this.right = right;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            var leftExpr = left.AsExpression();
            var rightExpr = right.AsExpression();

            return ParameterRebinder.Compose(leftExpr, rightExpr, Expression.OrElse);
        }

    }
}
