namespace LogisticaAPI.Exceptions;

public class CarregamentoNaoEncontradoException : Exception
{
    public Guid CarregamentoId { get; private set; }

    public CarregamentoNaoEncontradoException(Guid carregamentoId) : base(
        $"Carregamneto {carregamentoId} nao encontrado")
    {
        CarregamentoId = carregamentoId;
    }
}