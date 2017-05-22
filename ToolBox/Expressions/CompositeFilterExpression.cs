using System;
using System.Linq.Expressions;
using ToolBox.Utils;

namespace ToolBox.Expressions
{
    public abstract class CompositeFilterExpression<T> where T : class
    {
        public abstract Expression<Func<T, bool>> AsExpression();

        protected CompositeFilterExpression<T> And(CompositeFilterExpression<T> other)
        {
            other.Required();

            return new AndFilterExpression<T>(this, other);
        }

        protected CompositeFilterExpression<T> Or(CompositeFilterExpression<T> other)
        {
            other.Required();

            return new OrFilterExpression<T>(this, other);
        }

        protected CompositeFilterExpression<T> Not()
        {
            return new NotFilterExpression<T>(this);
        }

        public static implicit operator CompositeFilterExpression<T>(Expression<Func<T, bool>> expr)
        {
            if (expr == null) return null;

            return new FilterExpression<T>(expr);
        }

        public static implicit operator Expression<Func<T, bool>>(CompositeFilterExpression<T> str)
        {
            if (str == null) return null;

            return str.AsExpression();
        }

        public static implicit operator Func<T, bool>(CompositeFilterExpression<T> str)
        {
            if (str == null) return null;

            return str.AsExpression().Compile();
        }

        public static CompositeFilterExpression<T> operator &(CompositeFilterExpression<T> x, CompositeFilterExpression<T> y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return null;

            return x.And(y);
        }

        public static CompositeFilterExpression<T> operator |(CompositeFilterExpression<T> x, CompositeFilterExpression<T> y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return null;

            return x.Or(y);
        }

        public static CompositeFilterExpression<T> operator !(CompositeFilterExpression<T> x)
        {
            if (ReferenceEquals(x, null))
                return null;

            return x.Not();
        }
    }
}
