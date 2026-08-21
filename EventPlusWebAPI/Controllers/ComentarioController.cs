using EventPlusWebAPI.DTO;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentario _comentario;

        public ComentarioController(IComentario comentario)
        {
            _comentario = comentario;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _comentario.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Comentar([FromBody] ComentarioDTO DTO)
        {
            try
            {
                var comentario = new Comentario()
                {
                    DataComentario = DTO.DataComentario,
                    Descricao = DTO.Descricao,
                    IdEvento = DTO.IdEvento,
                    IdUsuario = DTO.IdUsuario
                };

                await _comentario.Comentar(comentario);

                return StatusCode(
                    201,
                    "Comentário cadastrado com sucesso."
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
                var comentarioBuscado = await _comentario.BuscarPorId(id);

                if (comentarioBuscado == null)
                {
                    return NotFound("Comentário não encontrado.");
                }

                return Ok(comentarioBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(
            Guid id,
            [FromBody] ComentarioDTO DTO)
        {
            try
            {
                var comentario = new Comentario()
                {
                    IdComentario = id,
                    DataComentario = DTO.DataComentario,
                    Descricao = DTO.Descricao,
                    IdEvento = DTO.IdEvento,
                    IdUsuario = DTO.IdUsuario
                };

                await _comentario.Atualizar(id, comentario);

                return Ok("Comentário atualizado com sucesso.");
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
                await _comentario.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}