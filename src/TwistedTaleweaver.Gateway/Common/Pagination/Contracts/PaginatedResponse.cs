using System.ComponentModel.DataAnnotations;

namespace TwistedTaleweaver.Gateway.Common.Pagination.Contracts;

/// <summary>
/// Represents a paginated response
/// </summary>
public class PaginatedResponse<T>
{
    /// <summary>
    /// Items on the current page
    /// </summary>
    [Required]
    public IEnumerable<T> Items { get; set; }

    /// <summary>
    /// Current page number
    /// </summary>
    [Required]
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    [Required]
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of items across all pages
    /// </summary>
    [Required]
    public int TotalItems { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    [Required]
    public int TotalPages { get; set; }

    public PaginatedResponse(IEnumerable<T> items, int totalItems, int pageNumber, int pageSize)
    {
        Items = items;
        TotalItems = totalItems;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }
}