
namespace LogisticaAPI.DTOs.Paginacao;

public class PagedResult<T>
{
    public IReadOnlyList<T> Itens { get; set; } = [];
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public PagedResult<TDestino> Map<TDestino>(Func<T, TDestino> seletor) => new()
    {
        Itens = Itens.Select(seletor).ToList(),
        PageNumber = PageNumber,
        PageSize = PageSize,
        TotalCount = TotalCount,
    };


}