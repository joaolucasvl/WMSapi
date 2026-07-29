using LogisticaAPI.Common;
using LogisticaAPI.DTOs.AutenticacaoDTOs;
using LogisticaAPI.Entities.Autenticacao;
using LogisticaAPI.Repositories.AuthRepositories;
using Microsoft.Extensions.Options;

namespace LogisticaAPI.Services.AuthServices;

public class ServicoAuth : IServicoAuth
{
    private readonly IUnitOfWork _uow;
    private readonly IServicoSenha _servicoSenha;
    private readonly IServicoToken _servicoToken;
    private readonly JwtOptions _opcoes;

    public ServicoAuth(
        IUnitOfWork uow,
        IServicoSenha servicoSenha,
        IServicoToken servicoToken,
        IOptions<JwtOptions> opcoes)
    {
        _uow = uow;
        _servicoSenha = servicoSenha;
        _servicoToken = servicoToken;
        _opcoes = opcoes.Value;
    }

    public async Task<Resultado<Usuario>> RegistrarAsync(RegistrarRequest request)
    {
        if (await _uow.Usuario.ObterPorEmailAsync(request.Email) is not null)
            return Resultado<Usuario>.Falha(ErroAuth.EmailJaCadastrado);

        var usuario = new Usuario
        {
            Nome = request.Nome,
            Email = request.Email,
            SenhaHash = _servicoSenha.Hash(request.Senha)
        };

        _uow.Usuario.AdicionarUsuario(usuario);
        await _uow.CommitAsync();

        return Resultado<Usuario>.Ok(usuario);
    }

    public async Task<Resultado<TokenResponse>> LoginAsync(LoginRequest request)
    {
        var usuario = await _uow.Usuario.ObterPorEmailAsync(request.Email);

        if (usuario is null || !_servicoSenha.Verificar(request.Senha, usuario.SenhaHash))
            return Resultado<TokenResponse>.Falha(ErroAuth.CredenciaisInvalidas);

        var resposta = EmitirTokens(usuario);
        await _uow.CommitAsync();

        return Resultado<TokenResponse>.Ok(resposta);
    }

    public async Task<Resultado<TokenResponse>> AtualizarAsync(RefreshRequest request)
    {
        var atual = await _uow.RefreshToken.ObterPorTokenAsync(request.RefreshToken);

        if (atual is null)
            return Resultado<TokenResponse>.Falha(ErroAuth.RefreshTokenInvalida);

        // Token já revogado sendo reapresentado: trata como vazamento e derruba a sessão inteira.
        if (atual.RevogadoEm is not null)
        {
            await _uow.RefreshToken.RevogarTodosDoUsuarioAsync(atual.UsuarioId);
            await _uow.CommitAsync();
            return Resultado<TokenResponse>.Falha(ErroAuth.SessaoEncerrada);
        }

        if (DateTime.UtcNow >= atual.ExpiraEm)
            return Resultado<TokenResponse>.Falha(ErroAuth.RefreshExpirado);

        var resposta = EmitirTokens(atual.Usuario);

        atual.RevogadoEm = DateTime.UtcNow;
        atual.SubstituidoPor = resposta.RefreshToken;

        await _uow.CommitAsync();

        return Resultado<TokenResponse>.Ok(resposta);
    }

    public async Task<Resultado<bool>> LogoutAsync(int usuarioId)
    {
        if (await _uow.Usuario.ObterPorIdAsync(usuarioId) is null)
            return Resultado<bool>.Falha(ErroAuth.UsuarioNaoEncontrado);

        await _uow.RefreshToken.RevogarTodosDoUsuarioAsync(usuarioId);
        await _uow.CommitAsync();

        return Resultado<bool>.Ok(true);
    }

    private TokenResponse EmitirTokens(Usuario usuario)
    {
        var (accessToken, expiraEm) = _servicoToken.GerarAccessToken(usuario);

        var refreshToken = new RefreshToken
        {
            Token = _servicoToken.GerarRefreshToken(),
            UsuarioId = usuario.Id,
            ExpiraEm = DateTime.UtcNow.AddDays(_opcoes.RefreshTokenDias)
        };

        _uow.RefreshToken.Adicionar(refreshToken);

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiraEm = expiraEm
        };
    }
}
