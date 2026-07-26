using LogisticaAPI.Entities;
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
}