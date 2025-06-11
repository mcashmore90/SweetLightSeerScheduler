
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SweetLightSeerScheduler.Models
{
    public class Result<T>
    {
        public bool IsSuccessful { get; private set; }
        public T Data { get; private set; }

        public string ErrorMessage { get; private set; }

        private Result(bool isSuccessful, T data, string message)
        {
            IsSuccessful = isSuccessful;
            Data = data;
            ErrorMessage = message;
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true,data, "");
        }

        public static Result<T> Failure(string message)
        {
            return new Result<T>(false, default(T), message);
        }
    }
}
