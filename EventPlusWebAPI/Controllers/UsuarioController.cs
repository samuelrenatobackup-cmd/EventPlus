using EventPlusWebAPI.DTO;
using EventPlusWebAPI.Interfaces;

using EventPlusWebAPI.Models;
using Microsoft.AspNetCore.Http;
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
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO dto)
        {
            try
            {
                var usuario = new UsuarioDTO
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha, //obs: a criptografia 
                    IdTipoUsuario = dto.IdTipoUsuario

                };

                await _usuario.Cadastrar(usuario);

                return StatusCode(201, usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Usuario usuario)
        {
            try
            {
                await _usuario.Atualizar(id, usuario);

                usuario.IdUsuario = id;

                return Ok(usuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
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
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}