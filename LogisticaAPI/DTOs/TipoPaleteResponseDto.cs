namespace LogisticaAPI.DTOs;

public class TipoPaleteResponseDto
{
    public Guid TipoPaleteId { get; set; }
    public string? Nome { get; set; }
    public double PesoMaximo { get; set; }
    public double AlturaMaximo { get; set; }
    public double VolumeMaximo { get; set; }
}