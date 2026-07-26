namespace LogisticaAPI.Entities;

public class TipoPalete
{
    public Guid TipoPaleteId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public double PesoMaximo { get; set; }
    public double AlturaMaximo { get; set; }
    public double VolumeMaximo { get; set; }
}