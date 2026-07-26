using LogisticaAPI.Enums;

namespace LogisticaAPI.Entities;

public class Carregamento
{
    public Guid CarregamentoId { get; set; }
    public DateTime CriadoEm { get; set; }
    public string? Transportadora { get; set; } = string.Empty;
    public string? Rota { get; set; }
    public StatusCarregamento Status { get; set; } = StatusCarregamento.Pendente;
    public DateTime DataDeChegada { get; set; }
    public DateTime DataDeCarregamento { get; set; }
    public decimal? PesoTotal { get; set; }
    public string ModeloCaminhao { get; set; }
    
    public ICollection<Palete> Paletes { get; set; } = new List<Palete>();
}