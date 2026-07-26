using LogisticaAPI.Enums;

namespace LogisticaAPI.Entities;

public class Pedido
{
    public int PedidoId { get; set; }
    public TipoPedido TipoPedido { get; set; }
    public DateTime CriadoEm { get; set; }
    public string Cliente { get; set; } = string.Empty;
    
    
    public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    
}