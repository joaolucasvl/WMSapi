
using System.ComponentModel.DataAnnotations;

namespace LogisticaAPI.DTOs;

public class AlocacaoRequestDto
{
    [Required]
    public int ItemPedidoId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantidade { get; set; }
}