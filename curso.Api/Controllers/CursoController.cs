using curso.Api.Business.Entities;
using curso.Api.Business.Repositories;
using curso.Api.Models.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace curso.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        /// <summary>
        /// Este serviço permite cadastrar cursos para usuário autenticado
        /// </summary>
        /// <param name="cursoViewModelInput"></param>
        /// <returns>Retornar status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Cadastrar um curso ", Type = typeof(CursoViewModelInput))]
        [SwaggerResponse(statusCode: 401, description: "Não Autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var curso = new Curso
            {
                CodigoUsuario = codigoUsuario,
                Nome = cursoViewModelInput.Nome,
                Descricao = cursoViewModelInput.Descricao
            };

            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();

            return Created("", cursoViewModelInput);
        }

        /// <summary>
        /// Este serviço permite recuperar o cursos 
        /// </summary>       
        /// <returns>Retornar status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao obter um curso ", Type = typeof(CursoViewModelInput))]
        [SwaggerResponse(statusCode: 401, description: "Não Autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var cursos = _cursoRepository.ObterPorUsuario(codigoUsuario)
                .Select(s => new CursoViewModelOutPut
                {
                    Login = s.Usuario.Login,
                    Nome = s.Nome,
                    Descricao = s.Descricao
                });

            return Ok(cursos);
        }
    }
}
