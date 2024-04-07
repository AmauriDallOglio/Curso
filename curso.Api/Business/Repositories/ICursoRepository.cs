using curso.Api.Business.Entities;

namespace curso.Api.Business.Repositories
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        IList<Curso> ObterPorUsuario(int codigoUsuario);
        void Commit();
    }
}