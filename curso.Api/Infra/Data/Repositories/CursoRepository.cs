using curso.Api.Business.Entities;
using curso.Api.Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace curso.Api.Infra.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext _context;

        public CursoRepository(CursoDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Curso curso)
        {
            _context.Cursos.Add(curso);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IList<Curso> ObterPorUsuario(int codigoUsuario)
        {
            return _context.Cursos
                .Include(i => i.Usuario)
                .Where(c => c.CodigoUsuario == codigoUsuario)
                .ToList();
        }
    }
}
