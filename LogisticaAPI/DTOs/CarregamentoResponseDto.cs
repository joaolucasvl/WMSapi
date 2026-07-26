using LogisticaAPI.Enums;

namespace LogisticaAPI.DTOs;

public class CarregamentoResponseDto
{
    public Guid CarregamentoId { get; set; }
    public DateTime CriadoEm { get; set; }
    public string? Transportadora { get; set; }
    public string? Rota { get; set; }
    public StatusCarregamento Status { get; set; }
    public DateTime DataDeChegada { get; set; }
    public DateTime DataDeCarregamento { get; set; }
    public decimal? PesoTotal { get; set; }
    public string ModeloCaminhao { get; set; } = string.Empty;
}