using LogisticaAPI.Enums;

namespace LogisticaAPI.DTOs;

public class PedidoResponseDto
{
    public int PedidoId { get; set; }
    public TipoPedido TipoPedido { get; set; }
    public DateTime CriadoEm { get; set; }
    public string Cliente { get; set; } = string.Empty;

    public List<ItemPedidoResponseDto> Itens { get; set; } = new();
}