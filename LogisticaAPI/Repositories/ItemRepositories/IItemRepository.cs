using LogisticaAPI.DTOs.Paginacao;
using LogisticaAPI.Entities;

namespace LogisticaAPI.Repositories.ItemRepositories;

public interface IItemRepository
{
    Task <Item?> GetbyId(int id);
    Task<PagedResult<Item>> GetPaged(QueryableParameters parametros);
    Task <bool> Delete(int id);
    Task <Item> Add(Item item);
    Task <Item?> Update(int id,Item item);   
}