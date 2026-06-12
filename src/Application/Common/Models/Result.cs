using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Result
    {
        public bool Success { get; init; }

        public string? Error { get; init; }

        public static Result Ok()
            => new() { Success = true };

        public static Result Failure(string error)
            => new()
            {
                Success = false,
                Error = error
            };
    }

    public class Result<T> : Result
    {
        public T? Data { get; init; }

        public static Result<T> Ok(T data)
            => new()
            {
                Success = true,
                Data = data
            };

        public new static Result<T> Failure(string error)
            => new()
            {
                Success = false,
                Error = error
            };
    }
}
