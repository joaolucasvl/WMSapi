using System.ComponentModel.DataAnnotations;
using LogisticaAPI.Enums;

namespace LogisticaAPI.DTOs;

public class PedidoRequestDto
{
    [Required(ErrorMessage = "O Tipo do Pedido é obrigatorio.")]
    public TipoPedido TipoPedido { get; set; }
    
    [Required(ErrorMessage = "O nome do Cliente é obrigatorio.")]
    [StringLength(30), MinLength(3)]
    public string Cliente { get; set; } = string.Empty;
    
    public List<ItemPedidoRequestDto> Itens { get; set; } = new();
}