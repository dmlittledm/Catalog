using ToolBox.Utils;
using System;
using System.Text;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ToolBox.Functional
{
    public class Result
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }

        public bool Failure
        {
            get { return !Success; }
        }

        protected Result(bool success, string error)
        {
            if (!success && string.IsNullOrEmpty(error))
                throw new ArgumentException("Не указан текст ошибки для ошибочной операции.");

            if (success && !string.IsNullOrEmpty(error))
                throw new ArgumentException("Указан текст ошибки для успешной операции.");


            Success = success;
            Error = error;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static Result Combine(params Result[] results)
        {
            return Combine(null, results);
        }

        public static Result Combine(string listStyle, params Result[] results)
        {
            if (results == null) return null;

            var sb = new StringBuilder();
            var lstStyle = listStyle.OnEmpty("");

            foreach (var result in results.Where(result => result.Failure))
            {
                sb.AppendLine(lstStyle + result.Error);
            }

            return sb.Length > 0 ? Fail(sb.ToString()) : Ok();
        }
    }

    public class Result<T> : Result
    {
        private T value;

        public T Value
        {
            get
            {
                if (!Success) throw new InvalidOperationException("Во время выполнения операции произошла ошибка проверьте поле \"Error\".");

                Contract.Assume(value != null);

                return value;
            }
            private set 
            {
                this.value = value; 
            }
        }

        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            if (success && value == null)
                throw new ArgumentException("Для успешной операции необходимо установить значение.");

            Value = value;
        }
    }
}
