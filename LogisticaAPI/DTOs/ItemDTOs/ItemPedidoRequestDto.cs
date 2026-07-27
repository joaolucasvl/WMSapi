using System.ComponentModel.DataAnnotations;

namespace LogisticaAPI.DTOs;

public class ItemPedidoRequestDto
{
    public int ItemPedidoId { get; set; }
    public int ItemId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantidade { get; set; }
}