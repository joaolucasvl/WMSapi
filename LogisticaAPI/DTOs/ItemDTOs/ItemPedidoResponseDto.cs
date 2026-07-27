namespace LogisticaAPI.DTOs;

public class ItemPedidoResponseDto
{
    public int ItemPedidoId { get; set; }
    public int ItemId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public double PesoUnitario { get; set; }
    public double VolumeUnitario { get; set; }
    public double AlturaUnitario { get; set; }
    public double PesoTotal { get; set; }
    public double VolumeTotal { get; set; }
}