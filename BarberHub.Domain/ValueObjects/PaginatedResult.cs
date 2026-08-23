using System.Data;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.ValueObjects;

public record PaginatedResult<T>
{
    public IReadOnlyCollection<T> Items { get; private init; }
    public int PageNumber { get; private init; }
    public int PageSize { get; private init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int TotalCount { get; private init; }

    public PaginatedResult(IReadOnlyCollection<T> items, int pageNumber, int pageSize, int totalCount)
    {
        ValidatePageNumber(pageNumber);
        ValidatePageSize(pageSize);
        ValidateTotalCount(totalCount);
        Items = items ?? throw new RequiredFieldException(nameof(items));
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    private static void ValidatePageNumber(int pageNumber)
    {
        if (pageNumber < 1) throw new InvalidPageNumberException();
    }

    private static void ValidatePageSize(int pageSize)
    {
        if (pageSize < 1) throw new InvalidPageSizeException();
    }

    private static void ValidateTotalCount(int pageCount)
    {
        if (pageCount < 0) throw new InvalidTotalCountException();
    }
}