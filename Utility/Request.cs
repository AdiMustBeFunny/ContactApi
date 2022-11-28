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
        public string ErrorMessage { get; }
        public Dictionary<string,string> PropertyErrors { get; }

        private Request(T value,bool isSuccessful, string error, Dictionary<string, string> propertyErrors)
        {
            Value = value;
            IsSuccessful = isSuccessful;
            ErrorMessage = error;
            PropertyErrors = propertyErrors;
        }

        public static Request<T> Success(T value)
        {
            return new(value, true, null, null);
        }

        public static Request<T> Failure(string error, Dictionary<string, string> propertyErrors = null)
        {
            return new(null, false, error, propertyErrors);
        }

        public static Request<T> Failure(Dictionary<string, string> propertyErrors)
        {
            return new(null, false, null, propertyErrors);
        }

        public static Request<T> Failure(string fieldName, string fieldError)
        {
            var propertyErrors = new Dictionary<string, string>();
            propertyErrors.Add(fieldName, fieldError);

            return new(null, false, null, propertyErrors);
        }
    }
}
