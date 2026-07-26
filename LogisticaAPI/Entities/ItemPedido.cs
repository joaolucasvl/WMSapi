namespace LogisticaAPI.Entities;

public class ItemPedido
{
    public int ItemPedidoId { get; set; }
    public int Quantidade { get; set; }
    
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;
    
    
    public string Descricao { get; set; } = string.Empty;
    public double VolumeUnitario { get; set; }
    public double PesoUnitario { get; set; }
    public double AlturaUnitario { get; set; }
    
    public ICollection<ItemPalete> Alocacoes { get; set; } = new List<ItemPalete>();
    
}