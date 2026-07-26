namespace LogisticaAPI.Entities;

public class ItemPalete
{
    public int ItemPaleteId { get; set; }
    
    public int ItemPedidoId { get; set; }
    public ItemPedido ItemPedido { get; set; } = null!;
    
    public int PaleteId { get; set; }
    public Palete Palete { get; set; } = null!;
    
    public int Quantidade { get; set; }
}