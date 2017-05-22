using System;
using System.Linq.Expressions;
using ToolBox.Utils;

namespace ToolBox.Expressions
{
    public class FilterExpression<T> : CompositeFilterExpression<T> where T : class
    {
        private readonly Expression<Func<T, bool>> expression;

        public FilterExpression(Expression<Func<T, bool>> expression)
        {
            expression.Required();

            this.expression = expression;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            return expression;
        }
    }
}
