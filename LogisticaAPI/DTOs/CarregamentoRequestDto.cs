using System.ComponentModel.DataAnnotations;
using LogisticaAPI.Enums;

namespace LogisticaAPI.DTOs;

public class CarregamentoRequestDto
{
    [Required(ErrorMessage = "O nome da Transportadora é obrigatorio.")]
    [StringLength(30), MinLength(3)]
    public string? Transportadora { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "O nome da Rota é obrigatorio.")]
    [StringLength(20), MinLength(6)]
    public string? Rota { get; set; }
    
    public DateTime DataDeChegada { get; set; }
    public DateTime DataDeCarregamento { get; set; }
    public decimal? PesoTotal { get; set; }
    public string ModeloCaminhao { get; set; } = string.Empty;
}