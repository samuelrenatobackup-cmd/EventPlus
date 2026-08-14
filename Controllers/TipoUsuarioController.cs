using EventPlusWebAPI.DTO;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly ITipoUsuario _tipoUsuario;

        public TipoUsuarioController(ITipoUsuario tipoUsuario)
        {
            _tipoUsuario = tipoUsuario;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _tipoUsuario.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] TipoUsuarioDTO DTO)
        {
            try
            {
                var tipoUsuario = new TipoUsuario()
                {
                    TituloTipoUsuario = DTO.Titulo
                };

                await _tipoUsuario.Cadastrar(tipoUsuario);

                return StatusCode(
                    201,
                    "Tipo de usuário cadastrado com sucesso " + tipoUsuario.TituloTipoUsuario
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
                var tipoUsuarioBuscado = await _tipoUsuario.BuscarPorId(id);

                if (tipoUsuarioBuscado == null)
                {
                    return NotFound("Tipo de usuário não encontrado.");
                }

                return Ok(tipoUsuarioBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _tipoUsuario.Deletar(id);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] TipoUsuarioDTO DTO)
        {
            try
            {
                var tipoUsuario = new TipoUsuario()
                {
                    IdTipoUsuario = id,
                    TituloTipoUsuario = DTO.Titulo
                };

                await _tipoUsuario.Atualizar(id, tipoUsuario);

                return Ok("Tipo de usuário atualizado com sucesso.");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
