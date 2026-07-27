using LogisticaAPI.DTOs.Paginacao;
using LogisticaAPI.Entities;

namespace LogisticaAPI.Repositories.PaleteRepositories;

public interface IPaleteRepository
{
    Task <Palete> GetbyId(int id);
    Task<PagedResult<Palete>> GetPaged(QueryableParameters parametros);
    Task <bool> Delete(int id);
    Task <Palete> Add(Palete palete);
    Task <Palete?> Update(int id,Palete pedido);  
    Task<ItemPalete> AdicionarAlocacao(ItemPalete alocacao);
    Task<ItemPalete> RemoverAlocacao(ItemPalete alocacao);
    Task<ItemPalete?> GetAlocacaoById(int id);
}