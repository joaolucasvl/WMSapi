namespace LogisticaAPI.DTOs.Paginacao;

public class QueryableParameters
{
    const int MaxPageSize = 50;
    private int _pageNumber { get; set; } = 1;
    private int _pageSize { get; set; } = 10;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value switch
        {
            < 1 => 1,
            > MaxPageSize => MaxPageSize,
            _ => value
        };
    }
}