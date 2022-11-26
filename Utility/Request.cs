using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public struct Request<T> where T : class
    {
        public bool IsSuccessful { get; }
        public T Value { get; }
        public List<string> Errors { get; }

        private Request(T value,bool isSuccessful, List<string> errors)
        {
            Value = value;
            IsSuccessful = isSuccessful;
            Errors = errors;
        }

        public static Request<T> Success(T value)
        {
            return new(value, true, null);
        }

        public static Request<T> Failure(string error)
        {
            return new(null, false, new List<string>() { error });
        }

        public static Request<T> Failure(List<string> errors)
        {
            return new(null, false, errors);
        }
    }
}
