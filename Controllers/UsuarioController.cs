using EventPlusWebAPI.DTO;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuario;

        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _usuario.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO DTO)
        {
            try
            {
                var usuario = new Usuario()
                {
                 
                     Nome = DTO.Nome,
                     Email = DTO.Email,
                     Senha = DTO.Senha
                };

                await _usuario.Cadastrar(usuario);

                return StatusCode(
                    201,
                    "Usuário cadastrado com sucesso."
                );
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            try
            {
                var usuarioBuscado = await _usuario.BuscarPorId(id);

                if (usuarioBuscado == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok(usuarioBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _usuario.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(
            Guid id,
            [FromBody] UsuarioDTO DTO
        )
        {
            try
            {
                var usuario = new Usuario()
                {
                    IdUsuario = id,

                     Nome = DTO.Nome,
                     Email = DTO.Email,
                     Senha = DTO.Senha
                };

                await _usuario.Atualizar(id, usuario);

                return Ok("Usuário atualizado com sucesso.");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}