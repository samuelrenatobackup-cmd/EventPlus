
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEventoController : ControllerBase
    {
        private readonly ITipoEvento _tipoEvento;

        public TipoEventoController(ITipoEvento tipoEvento)
        {
            _tipoEvento = tipoEvento;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _tipoEvento.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] TipoEvento tipoEvento)
        {
            try
            {
                await _tipoEvento.Cadastrar(tipoEvento);

                return StatusCode(
                    201,
                    "Tipo de evento cadastrado com sucesso " + tipoEvento.TituloTipoEvento
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
                var tipoEventoBuscado = await _tipoEvento.BuscarPorId(id);

                if (tipoEventoBuscado == null)
                {
                    return NotFound("Tipo de evento não encontrado.");
                }

                return Ok(tipoEventoBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _tipoEvento.Deletar(id);
            return NoContent();
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] TipoEvento tipoEvento)
        {
            try
            {
                await _tipoEvento.Atualizar(id, tipoEvento);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
