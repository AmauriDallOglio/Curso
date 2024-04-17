using curso.Api.Business.Entities;
using curso.Api.Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace curso.Api.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CursoDbContext _context;

        public UsuarioRepository(CursoDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task<Usuario> ObterUsuarioAsync(string login)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}

