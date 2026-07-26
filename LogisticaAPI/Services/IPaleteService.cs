using LogisticaAPI.DTOs;
using LogisticaAPI.Entities;

namespace LogisticaAPI.Services;

public interface IPaleteService
{
    Task<ItemPalete> Alocar(int paleteId, AlocacaoRequestDto request);
    Task<Palete> CriarPalete(PaleteRequestDto  request);
    Task Desalocar(int itemPaleteId);
}