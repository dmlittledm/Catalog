using System;
using ToolBox.Utils;

namespace ToolBox.Functional
{
    public static class ResultExtensions
    {
        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            result.Required();
            func.Required();

            if (result.Failure)
                return result;

            return func();
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            result.Required();
            action.Required();

            if (result.Failure)
                return result;

            action();

            return Result.Ok();
        }

        public static Result OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            result.Required();
            action.Required();

            if (result.Failure)
                return result;

            action(result.Value);

            return Result.Ok();
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
        {
            result.Required();
            func.Required();

            if (result.Failure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(func());
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            result.Required();
            func.Required();

            if (result.Failure)
                return Result.Fail<T>(result.Error);

            return func();
        }

        public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> func)
        {
            result.Required();
            func.Required();

            if (result.Failure)
                return Result.Fail<TOut>(result.Error);

            return func(result.Value);
        }

        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            result.Required();
            func.Required();

            if (result.Failure)
                return result;

            return func(result.Value);
        }

        public static Result OnFailure(this Result result, Action action)
        {
            result.Required();
            action.Required();

            if (result.Failure)
            {
                action();
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action<Result> action)
        {
            result.Required();
            action.Required();

            if (result.Failure)
            {
                action(result);
            }

            return result;
        }

        public static Result OnBoth(this Result result, Action<Result> action)
        {
            result.Required();
            action.Required();

            action(result);

            return result;
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            result.Required();
            func.Required();

            return func(result);
        }

        public static TOut OnBoth<TIn, TOut>(this Result<TIn> result, Func<Result<TIn>, TOut> func)
        {
            result.Required();
            func.Required();

            return func(result);
        }

        public static void ThrowOnFail(this Result result)
        {
            result.Required();

            if(result.Failure)
                throw new Exception(result.Error);
        }
    }
}
