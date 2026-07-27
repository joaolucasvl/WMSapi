using LogisticaAPI.DTOs;
using LogisticaAPI.DTOs.Paginacao;
using LogisticaAPI.Entities;

namespace LogisticaAPI.Services.PaleteServices;

public interface IPaleteService
{
    Task<ItemPalete> Alocar(int paleteId, AlocacaoRequestDto request);
    Task<Palete> CriarPalete(PaleteRequestDto  request);
    Task Desalocar(int itemPaleteId);
    Task<Palete> GetById(int paleteId);
    Task<PagedResult<Palete>> GetPaged(QueryableParameters parametros);
}