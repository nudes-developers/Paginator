using System;
namespace Nudes.Paginator.Core;

/// <summary>
/// Exception for when trying to sort a collection with an invalid property
/// </summary>
public class InvalidSortingException : Exception
{
    /// <summary>
    /// </summary>
    /// <param name="fieldOrProperty"></param>
    public InvalidSortingException(string fieldOrProperty) : base($"Member {fieldOrProperty} is not sortable or does not exist")
    {
        FieldOrProperty=fieldOrProperty;
    }

    /// <summary>
    /// Member that invoked this exception
    /// </summary>
    public string FieldOrProperty { get; set; }
}
