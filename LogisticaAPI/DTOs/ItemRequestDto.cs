using System.ComponentModel.DataAnnotations;

namespace LogisticaAPI.DTOs;

public class ItemRequestDto
{
    [Required(ErrorMessage = "O nome é obrigatorio.")]
    [StringLength(30), MinLength(3)]
    public string Nome { get; set; } = string.Empty;
    
    
    [StringLength(30), MinLength(3)]
    public string Descricao { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "O nome do Fornecedor é obrigatorio.")]
    [StringLength(30), MinLength(3)]
    public string Fornecedor { get; set; } =  string.Empty;
    
    [Required(ErrorMessage = "Digite se o item é Perecivel!")]
    public bool Perecivel { get; set; } = false;
    
    [Range(0, int.MaxValue)]
    public double VolumeUnitario { get; set; } 
    
    [Range(0, int.MaxValue)]
    public double PesoUnitario { get; set; } 
    
    [Range(0, int.MaxValue)]
    public double AlturaUnitario { get; set; } 
}