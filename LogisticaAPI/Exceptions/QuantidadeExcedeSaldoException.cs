namespace LogisticaAPI.Exceptions;

public class QuantidadeExcedeSaldoException : Exception
{
    public QuantidadeExcedeSaldoException(int itemPedidoId, int solicitado, int saldo) : base($"Quantidade {solicitado} excede o saldo disponivel ({saldo})" + 
        $"do item de pedido {itemPedidoId}.")
    { }
}