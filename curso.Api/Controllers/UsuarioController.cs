using curso.Api.Business.Entities;
using curso.Api.Business.Repositories;
using curso.Api.Filters;
using curso.Api.Infra.Data;
using curso.Api.Models;
using curso.Api.Models.Usuarios;
using curso.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace curso.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationServices _authenticationService;

        public UsuarioController(IUsuarioRepository usuarioRepository, IAuthenticationServices authenticationService)
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


            //var usuario = _usuarioRepository.ObterUsuario(loginViewModelInput.Login);
            //if (usuario == null)
            //{
            //    return BadRequest("Usuário não localizado!");
            //}

            //var usuarioViewModelOutPut = new UsuarioViewModelOutPut()
            //{
            //    Codigo = usuario.Codigo,
            //    Login = usuario.Login,
            //    Email = usuario.Email
            //};

            //var token = _authenticationService.GerarToken(usuarioViewModelOutPut);

            //return Ok(new
            //{
            //    Token = token,
            //    Usuario = usuario
            //});
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
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder.UseSqlServer("Server=SERVER;Database=Curso;Trusted_Connection=True;Encrypt=False");
            CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);
            var migracoesPendentes = contexto.Database.GetPendingMigrations();
            if (migracoesPendentes.Count() > 0 )
            {
                contexto.Database.Migrate();
            }

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
