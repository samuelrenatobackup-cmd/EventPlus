using EventPlusWebAPI.DTO;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresencaController : ControllerBase
    {
        private readonly IPresenca _presenca;

        public PresencaController(IPresenca presenca)
        {
            _presenca = presenca;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var presencas = await _presenca.Listar();

            return Ok(presencas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var presenca = await _presenca.BuscarPorId(id);

            if (presenca == null)
            {
                return NotFound("Presença não encontrada.");
            }

            return Ok(presenca);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(PresencaDTO presencaDTO)
        {
            var presenca = new Presenca
            {
                IdPresenca = Guid.NewGuid(),
                Situacao = presencaDTO.Situacao,
                IdEvento = presencaDTO.IdEvento,
                IdUsuario = presencaDTO.IdUsuario
            };

            await _presenca.Cadastrar(presenca);

            return Created("", presenca);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            Guid id,
            PresencaDTO presencaDTO)
        {
            var presenca = await _presenca.BuscarPorId(id);

            if (presenca == null)
            {
                return NotFound("Presença não encontrada.");
            }

            presenca.Situacao = presencaDTO.Situacao;
            presenca.IdEvento = presencaDTO.IdEvento;
            presenca.IdUsuario = presencaDTO.IdUsuario;

            await _presenca.Atualizar(id, presenca);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var presenca = await _presenca.BuscarPorId(id);

            if (presenca == null)
            {
                return NotFound("Presença não encontrada.");
            }

            await _presenca.Deletar(id);

            return NoContent();
        }
    }
}