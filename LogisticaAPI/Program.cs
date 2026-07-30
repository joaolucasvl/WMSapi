using System.Text;
using LogisticaAPI.Common;
using LogisticaAPI.Data;
using LogisticaAPI.Exceptions;
using LogisticaAPI.Repositories;
using LogisticaAPI.Repositories.AuthRepositories;
using LogisticaAPI.Repositories.PaleteRepositories;
using LogisticaAPI.Repositories.CarregamentoRepositories;
using LogisticaAPI.Repositories.ItemRepositories;
using LogisticaAPI.Repositories.PedidoRepositories;
using LogisticaAPI.Services.AuthServices;
using LogisticaAPI.Services.PaleteServices;
using LogisticaAPI.Services.PedidoServices;   
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<ICarregamentoRepository, CarregamentoRepository>();
builder.Services.AddScoped<ITipoPaleteRepository, TipoPaleteRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPaleteRepository, PaleteRepository>();
builder.Services.AddScoped<IPaleteService, PaleteService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServicoSenha, ServicoSenha>();
builder.Services.AddScoped<IServicoToken, ServicoToken>();
builder.Services.AddScoped<IServicoAuth, ServicoAuth>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddOptions<JwtOptions>()
    .Bind(builder.Configuration.GetSection(JwtOptions.Secao))
    .Validate(o => o.Chave.Length >= 32,
        "Jwt:Chave ausente ou curta demais (mínimo 32 caracteres). Configure via user-secrets ou variável de ambiente Jwt__Chave.")
    .ValidateOnStart();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opcoes =>
    {
        var jwt = builder.Configuration.GetSection(JwtOptions.Secao).Get<JwtOptions>()!;

        opcoes.MapInboundClaims = false;
        opcoes.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Chave)),
            ClockSkew = TimeSpan.Zero,
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddOpenApi(opcoes =>
{
    opcoes.AddDocumentTransformer<TransformadorSegurancaJwt>();
    opcoes.AddOperationTransformer<TransformadorSegurancaOperacao>();
});
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

