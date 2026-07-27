using System.ComponentModel.DataAnnotations;

namespace LogisticaAPI.DTOs;

public class PaleteRequestDto
{
    
    [Required(ErrorMessage = "O numero do Palete é obrigatorio.")]
    [Range(1, int.MaxValue)]
    public int Numero { get; set; }
    public Guid TipoPaleteId { get; set; }     
    public Guid? CarregamentoId { get; set; }
}