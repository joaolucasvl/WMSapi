using Microsoft.EntityFrameworkCore;

namespace LogisticaAPI.DTOs.Paginacao;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> source, QueryableParameters parametros)
    {
        var total = await source.CountAsync();
        var itens = await source.Skip((parametros.PageNumber - 1) * parametros.PageSize)
            .Take(parametros.PageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Itens = itens,
            PageNumber = parametros.PageNumber,
            PageSize = parametros.PageSize,
            TotalCount = total,
        };
    }
}