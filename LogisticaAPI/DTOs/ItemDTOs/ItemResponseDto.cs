namespace LogisticaAPI.DTOs;

public class ItemResponseDto
{
    public int ItemId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Fornecedor { get; set; } =  string.Empty;
    public bool Perecivel { get; set; } = false;
    public double VolumeUnitario { get; set; } 
    public double PesoUnitario { get; set; } 
    public double AlturaUnitario { get; set; } 
    
}