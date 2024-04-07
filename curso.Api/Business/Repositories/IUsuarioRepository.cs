using curso.Api.Business.Entities;

namespace curso.Api.Business.Repositories
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        Usuario ObterUsuario(string login);
        void Commit();
    }
}
