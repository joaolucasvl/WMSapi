namespace LogisticaAPI.DTOs;

public class PaleteResponseDto
{
    public int PaleteId { get; set; }
    public int Numero { get; set; }

    public double PesoMaximo { get; set; }      
    public double VolumeMaximo { get; set; }
    public double AlturaMaxima { get; set; }

    public double PesoAtual { get; set; }        
    public double VolumeAtual { get; set; }

    public Guid? CarregamentoId { get; set; }
}