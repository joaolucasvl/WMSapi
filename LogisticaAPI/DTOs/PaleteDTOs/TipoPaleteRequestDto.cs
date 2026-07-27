using System.ComponentModel.DataAnnotations;

namespace LogisticaAPI.DTOs;

public class TipoPaleteRequestDto
{
    [Required(ErrorMessage = "O nome é obrigatorio.")]
    [StringLength(30), MinLength(3)]
    public string Nome { get; set; } = string.Empty;
    
    [Required]
    [Range(0, int.MaxValue)]
    public double PesoMaximo { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public double AlturaMaximo { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public double VolumeMaximo { get; set; }
}