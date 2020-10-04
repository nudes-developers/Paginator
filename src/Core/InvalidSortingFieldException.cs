using System;

namespace Nudes.Paginator.Core
{
    public class InvalidSortingFieldException : Exception
    {
        public string Field { get; set; }

        public InvalidSortingFieldException(string field) :base($"Invalid sorting field {field}")
        {
            Field = field;
        }

        public InvalidSortingFieldException(string field, Exception ex) : base($"Invalid sorting field {field}", ex)
        {
            Field = field;
        }
    }
}
