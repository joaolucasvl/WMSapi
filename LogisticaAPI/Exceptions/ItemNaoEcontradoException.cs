namespace LogisticaAPI.Exceptions;

public class ItemNaoEcontradoException : Exception
{
    public int ItemId { get; }
    
    public ItemNaoEcontradoException(int itemId) : base($"Item {itemId} não encontrado.")
    {
        ItemId = itemId;
    }
}