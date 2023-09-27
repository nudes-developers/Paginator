using System;

namespace Nudes.Paginator.Core;

/// <summary>
/// Sorting definition
/// </summary>
public class SortingDefinition
{
    /// <summary>
    /// Empty constructor for binding
    /// </summary>
    public SortingDefinition()
    {
        /* as is */
    }

    /// <summary>
    /// constructor with field and sorting direction
    /// </summary>
    public SortingDefinition(string field, SortDirection sortDirection)
    {
        if (string.IsNullOrWhiteSpace(field))
            throw new ArgumentException($"'{nameof(field)}' cannot be null or whitespace.", nameof(field));

        Field=field;
        SortDirection=sortDirection;
    }

    /// <summary>
    /// Field that should be sorted by, the field can be a complex property
    /// </summary>
    public string Field { get; set; }

    /// <summary>
    /// Sorting direction of the field
    /// </summary>
    public SortDirection SortDirection { get; set; }
}