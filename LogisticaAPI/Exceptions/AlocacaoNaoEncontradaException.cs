namespace LogisticaAPI.Exceptions;

public class AlocacaoNaoEncontradaException : Exception
{
    public int ItemPaleteId { get; set; }

    public AlocacaoNaoEncontradaException(int itemPaleteId) : base($"Alocacao {itemPaleteId} Não encontrado")
    {
        ItemPaleteId = itemPaleteId;
    }
}