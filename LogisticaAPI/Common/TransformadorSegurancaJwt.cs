using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace LogisticaAPI.Common;

/// <summary>
/// Declara o esquema Bearer no documento OpenAPI. Sem isso o Scalar nao
/// mostra o botao Authorize e nao ha onde colar o token.
/// </summary>
public class TransformadorSegurancaJwt : IOpenApiDocumentTransformer
{
    public const string Esquema = "Bearer";

    public Task TransformAsync(OpenApiDocument documento,
        OpenApiDocumentTransformerContext contexto, CancellationToken ct)
    {
        var esquema = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Cole apenas o token, sem a palavra Bearer."
        };

        documento.Components ??= new OpenApiComponents();
        documento.Components.SecuritySchemes
            ??= new Dictionary<string, IOpenApiSecurityScheme>();
        documento.Components.SecuritySchemes[Esquema] = esquema;

        return Task.CompletedTask;
    }
}

/// <summary>
/// Marca com cadeado apenas as operacoes que exigem autenticacao, lendo o
/// [Authorize] dos metadados do endpoint.
/// </summary>
public class TransformadorSegurancaOperacao : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operacao,
        OpenApiOperationTransformerContext contexto, CancellationToken ct)
    {
        var metadados = contexto.Description.ActionDescriptor.EndpointMetadata;

        var exigeAuth = metadados.OfType<IAuthorizeData>().Any()
                        && !metadados.OfType<IAllowAnonymous>().Any();

        if (exigeAuth)
        {
            operacao.Security =
            [
                new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference(
                        TransformadorSegurancaJwt.Esquema)] = []
                }
            ];
        }

        return Task.CompletedTask;
    }
}
