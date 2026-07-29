using System.Security.Claims;
using LogisticaAPI.Common;
using LogisticaAPI.DTOs.AutenticacaoDTOs;
using LogisticaAPI.Entities.Autenticacao;
using LogisticaAPI.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace LogisticaAPI.Controller;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IServicoAuth _servicoAuth;

    public AuthController(IServicoAuth servicoAuth)
    {
        _servicoAuth = servicoAuth;
    }

    private static UsuarioResponse MapToResponse(Usuario u)
    {
        return new UsuarioResponse
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            Perfil = u.Perfil
        };
    }

    [HttpPost]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UsuarioResponse>> Registrar(RegistrarRequest request)
    {
        var resultado = await _servicoAuth.RegistrarAsync(request);

        if (!resultado.Sucesso)
            return Erro(resultado.Erro);

        return Ok(MapToResponse(resultado.Valor!));
    }

    [HttpPost]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenResponse>> Login(LoginRequest request)
    {
        var resultado = await _servicoAuth.LoginAsync(request);

        if (!resultado.Sucesso)
            return Erro(resultado.Erro);

        return Ok(resultado.Valor);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenResponse>> Refresh(RefreshRequest request)
    {
        var resultado = await _servicoAuth.AtualizarAsync(request);

        if (!resultado.Sucesso)
            return Erro(resultado.Erro);

        return Ok(resultado.Valor);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout()
    {
        var resultado = await _servicoAuth.LogoutAsync(UsuarioId());

        if (!resultado.Sucesso)
            return Erro(resultado.Erro);

        return NoContent();
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    public ActionResult<UsuarioResponse> Eu()
    {
        return Ok(new UsuarioResponse
        {
            Id = UsuarioId(),
            Nome = User.FindFirstValue("name") ?? string.Empty,
            Email = User.FindFirstValue(JwtRegisteredClaimNames.Email) ?? string.Empty,
            Perfil = User.FindFirstValue("role") ?? string.Empty
        });
    }

    private int UsuarioId()
    {
        return int.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);
    }

    private ObjectResult Erro(ErroAuth erro) => erro switch
    {
        ErroAuth.EmailJaCadastrado =>
            Problem("E-mail já cadastrado.", statusCode: StatusCodes.Status409Conflict),
        ErroAuth.UsuarioNaoEncontrado =>
            Problem("Usuário não encontrado.", statusCode: StatusCodes.Status404NotFound),
        ErroAuth.CredenciaisInvalidas =>
            Problem("E-mail ou senha inválidos.", statusCode: StatusCodes.Status401Unauthorized),
        ErroAuth.RefreshTokenInvalida =>
            Problem("Refresh token inválido.", statusCode: StatusCodes.Status401Unauthorized),
        ErroAuth.RefreshExpirado =>
            Problem("Refresh token expirado.", statusCode: StatusCodes.Status401Unauthorized),
        ErroAuth.SessaoEncerrada =>
            Problem("Sessão encerrada por reuso de refresh token. Faça login novamente.",
                statusCode: StatusCodes.Status401Unauthorized),
        _ =>
            Problem("Erro de autenticação.", statusCode: StatusCodes.Status400BadRequest)
    };
}
