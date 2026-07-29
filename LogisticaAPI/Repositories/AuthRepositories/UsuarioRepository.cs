using LogisticaAPI.Data;
using LogisticaAPI.Entities.Autenticacao;
using Microsoft.EntityFrameworkCore;

namespace LogisticaAPI.Repositories.AuthRepositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _dbContext;
    
    public UsuarioRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Usuario?> ObterPorEmailAsync(string Email)
    {
        return await _dbContext.Usuarios.SingleOrDefaultAsync(x => x.Email == Email);
    }

    public async Task<Usuario?> ObterPorIdAsync(int id)
    {
        return await _dbContext.Usuarios.FindAsync(id).AsTask();
    }

    public void AdicionarUsuario(Usuario usuario)
    {
        _dbContext.Usuarios.Add(usuario);
    }
}