using LogisticaAPI.Entities;
using LogisticaAPI.Entities.Autenticacao;
using Microsoft.EntityFrameworkCore;

namespace LogisticaAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Carregamento>  Carregamentos => Set<Carregamento>();
    public DbSet<TipoPalete>  TipoPaletes => Set<TipoPalete>();
    public DbSet<Item>  Itens => Set<Item>();
    public DbSet<Pedido>  Pedidos => Set<Pedido>();
    public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();
    
    public DbSet<ItemPalete> ItensPalete => Set<ItemPalete>();
    public DbSet<Palete> Paletes => Set<Palete>();
    public DbSet<Usuario>   Usuarios => Set<Usuario>();
    public DbSet<RefreshToken>  RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Usuario>(e =>
        {
            e.Property(u => u.Email).IsRequired().HasMaxLength(200);
            e.Property(u => u.Nome).IsRequired().HasMaxLength(150);
            e.Property(u => u.SenhaHash).IsRequired();

            e.HasIndex(u => u.Email).IsUnique();
        });

        mb.Entity<RefreshToken>(e =>
        {
            e.HasIndex(u => u.Token).IsUnique();
            e.HasOne(u => u.Usuario)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(u => u.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}