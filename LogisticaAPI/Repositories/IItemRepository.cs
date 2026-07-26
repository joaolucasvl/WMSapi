using LogisticaAPI.Entities;

namespace LogisticaAPI.Repositories;

public interface IItemRepository
{
    Task <Item?> GetbyId(int id);
    Task<IEnumerable<Item?>> GetAll();
    Task <bool> Delete(int id);
    Task <Item> Add(Item item);
    Task <Item?> Update(int id,Item item);   
}