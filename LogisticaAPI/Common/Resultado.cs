namespace LogisticaAPI.Common;

public record Resultado<T>(T? Valor, ErroAuth Erro)
{
    public bool Sucesso => Erro == ErroAuth.Nenhum;
    public static Resultado<T> Ok(T Valor) => new(Valor, ErroAuth.Nenhum);
    public static Resultado<T> Falha(ErroAuth Erro) => new(default, Erro);
}