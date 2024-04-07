using curso.Api.Business.Entities;
using curso.Api.Business.Repositories;
using curso.Api.Filters;
using curso.Api.Models;
using curso.Api.Models.Usuarios;
using curso.Api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace curso.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationServices _authenticationService;

        public UsuarioController(
            IUsuarioRepository usuarioRepository,
            IAuthenticationServices authenticationService)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Rota que permite autenticar um usuário cadastrado
        /// </summary>      
        /// <returns>Retorna usuário e token em caso de sucesso </returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutPut))]
        [SwaggerResponse(statusCode: 500, description: "Sucesso ao autenticar", Type = typeof(ErroGenericoViewModel))]
        [HttpGet]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar()
        {
            var usuario = new UsuarioViewModelOutPut
            {
                Codigo = 1,
                Login = "amauri",
                Email = "amauri@amauri.com"
            };

            var token = _authenticationService.GerarToken(usuario);

            return Ok(new
            {
                Token = token,
                Usuario = usuario
            });
        }

        /// <summary>
        /// Rota que permite Registrar um usuário 
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao registrar", Type = typeof(RegistroViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutPut))]
        [SwaggerResponse(statusCode: 500, description: "Sucesso ao autenticar", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistroViewModelInput registro)
        {
            var usuario = new Usuario
            {
                Login = registro.Login,
                Senha = registro.Senha,
                Email = registro.Email
            };

            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();

            return Created("", registro);
        }
    }
}
