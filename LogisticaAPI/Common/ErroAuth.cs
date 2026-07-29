namespace LogisticaAPI.Common;

public enum ErroAuth
{
    Nenhum, 
    EmailJaCadastrado,
    CredenciaisInvalidas,
    RefreshTokenInvalida,
    SessaoEncerrada,
    RefreshExpirado,
    UsuarioNaoEncontrado
}