using System;
using System.Threading.Tasks;

namespace ToolBox.Functional
{
    public static class ResultAsyncExtensions
    {
        public static async Task<Result<T>> OnSuccessAsync<T>(this Result result, Func<Task<Result<T>>> func)
        {
            if(result.Failure)
                return Result.Fail<T>(result.Error);

            return await func();
        }

        public static async Task<Result> OnSuccessAsync(this Result result, Func<Task> func)
        {
            if (result.Failure)
                return result;

            await func();

            return Result.Ok();
        }

        public static async Task<Result> OnSuccessAsync<T>(this Task<Result<T>> resultTask, Func<T, Task<Result>> func)
        {
            var result = await resultTask;
            
            if (result.Failure)
                return result;

            return await func(result.Value);
        }

    }
}
