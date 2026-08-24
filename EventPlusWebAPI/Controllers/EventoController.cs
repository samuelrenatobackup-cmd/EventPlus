using EventPlusWebAPI.DTO;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EventoController : ControllerBase
    {
        private readonly IEvento _eventoRepository;

        public EventoController(IEvento eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDTO dto)
        {
            try
            {
                var evento = new Evento
                {
                    NomeEvento = dto.NomeEvento,
                    Descricao = dto.Descricao,
                    DataEvento = dto.DataEvento,
                    ImagemUrl = dto.ImagemUrl,
                    IdTipoEvento = dto.IdTipoEvento,
                    IdInstituicao = dto.IdInstituicao
                };

                await _eventoRepository.Cadastrar(evento);

                return StatusCode(201, evento);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _eventoRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var evento = await _eventoRepository.BuscarPorId(id);

                if (evento == null)
                    return NotFound("Evento não encontrado.");

                return Ok(evento);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, EventoDTO dto)
        {
            try
            {
                var evento = new Evento
                {
                    NomeEvento = dto.NomeEvento,
                    Descricao = dto.Descricao,
                    DataEvento = dto.DataEvento,
                    ImagemUrl = dto.ImagemUrl,
                    IdTipoEvento = dto.IdTipoEvento,
                    IdInstituicao = dto.IdInstituicao
                };

                await _eventoRepository.Atualizar(id, evento);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _eventoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}