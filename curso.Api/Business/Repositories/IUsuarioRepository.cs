using curso.Api.Business.Entities;

namespace curso.Api.Business.Repositories
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        Task<Usuario> ObterUsuarioAsync(string login);
        void Commit();
    }
}
