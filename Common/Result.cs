using System.Diagnostics.CodeAnalysis;

namespace UniversitetSystem.Common
{
    public class Result
    {
        [MemberNotNullWhen(false, nameof(Error))]
        public bool Success { get; }
        public string? Error { get; }

        private Result(bool success, string? error)
        {
            Success = success;
            Error = error;
        }

        public static Result Ok() => new Result(true, null);
        public static Result Fail(string error) => new Result(false, error);
    }

    public class Result<T>
    {
        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Error))]
        public bool Success { get; }
        public string? Error { get; }
        public T? Value { get; }

        private Result(bool success, T? value, string? error)
        {
            Success = success;
            Value = value;
            Error = error;
        }

        public bool TryGetValue([NotNullWhen(true)] out T? value)
        {
            if (Success)
            {
                value = Value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        public static Result<T> Ok(T value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new(true, value, null);
        }
        public static Result<T> Fail(string error) => new(false, default, error);
    }
}