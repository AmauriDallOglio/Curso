using System.ComponentModel.DataAnnotations;

namespace curso.Api.Models.Usuarios
{
    public class UsuarioViewModelOutPut
    {

        public int Codigo { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}