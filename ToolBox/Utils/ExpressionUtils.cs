using System;
using System.Linq.Expressions;

namespace ToolBox.Utils
{
    public enum CompareType
    {
        Equal = 1,
        Greater = 2,
        GreaterOrEqual = 3,
        Less =4,
        LessOrEqual = 5
    }

    public static class ExpressionUtils
    {
        public static Expression<Func<TObject, bool>> CreateComprasionLambda<TObject, TResult>(
            Expression<Func<TObject, TResult>> memberExpression,
            TResult constantValue, CompareType compare)
        {
            var parameter = memberExpression.Parameters;
            var body = CreateConstantComprasionExpression(memberExpression, constantValue, compare);

            return Expression.Lambda<Func<TObject, bool>>(body, parameter);
        }

        public static Expression CreateConstantComprasionExpression<TObject, TResult>(
            Expression<Func<TObject, TResult>> memberExpression,
            TResult constantValue, CompareType compare)
        {
            var constant = Expression.Constant(constantValue);
            var member = memberExpression.Body as MemberExpression;

            switch (compare)
            {
                case CompareType.Equal:
                    return Expression.Equal(member, constant);
                case CompareType.Greater:
                    return Expression.GreaterThan(member, constant);
                case CompareType.GreaterOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);
                case CompareType.Less:
                    return Expression.LessThan(member, constant);
                case CompareType.LessOrEqual:
                    return Expression.LessThanOrEqual(member, constant);
                default:
                    throw new InvalidOperationException("Unknown compare type.");
            }
        }


    }
}
