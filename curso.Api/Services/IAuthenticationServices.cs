using curso.Api.Models.Usuarios;

namespace curso.Api.Services
{
    public interface IAuthenticationServices
    {
        string GerarToken(UsuarioViewModelOutPut usuarioViewModelOut);
    }
}

