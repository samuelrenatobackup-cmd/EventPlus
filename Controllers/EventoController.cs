using EventPlusWebAPI.DTO;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEvento _evento;

        public EventoController(IEvento evento)
        {
            _evento = evento;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _evento.Listar());
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            try
            {
                var eventoBuscado = await _evento.BuscarPorId(id);

                if (eventoBuscado == null)
                {
                    return NotFound();
                }

                return Ok(eventoBuscado);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(
            Guid id,
            [FromBody] EventoDTO eventoDTO)
        {
            try
            {

                var evento = new Evento()
                {
                    IdEvento = id,
                    IdTipoEvento = eventoDTO.IdTipoEvento,
                    IdInstituicao = eventoDTO.IdInstituicao,
                    NomeEvento = eventoDTO.NomeEvento,
                    Descricao = eventoDTO.Descricao,
                    DataEvento = eventoDTO.DataEvento,
                    ImagemUrl = eventoDTO.ImagemUrl

                };

                await _evento.Atualizar(id, evento);

                return Ok(evento);

            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Cadastrar(
           Guid id,
           [FromBody] EventoDTO eventoDTO)
        {
            try
            {

                var evento = new Evento()
                {
                    IdEvento = id,
                    IdTipoEvento = eventoDTO.IdTipoEvento,
                    IdInstituicao = eventoDTO.IdInstituicao,
                    NomeEvento = eventoDTO.NomeEvento,
                    Descricao = eventoDTO.Descricao,
                    DataEvento = eventoDTO.DataEvento,
                    ImagemUrl = eventoDTO.ImagemUrl

                };

                await _evento.Cadastrar(evento);
               
                return Ok(evento);

            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _evento.Deletar(id);
                return Ok("Evento Deletado");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        }
    }