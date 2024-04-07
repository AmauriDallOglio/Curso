using System.ComponentModel.DataAnnotations;

namespace curso.Api.Models.Usuarios
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "Campo '{0}' é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo '{0}' é obrigatório")]
        public string Senha { get; set; }
    }
}