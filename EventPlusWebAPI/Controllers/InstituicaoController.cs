using EventPlusWebAPI.DTO;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaoController : ControllerBase
    {
        private readonly IInstituicao _instituicao;

        public InstituicaoController(IInstituicao instituicao)
        {
            _instituicao = instituicao;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _instituicao.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] InstituicaoDTO DTO)
        {
            try
            {
                var instituicao = new Instituicao()
                {
                    Cnpj = DTO.CNPJ,
                    NomeFantasia = DTO.NomeFantasia,
                    Endereco = DTO.Endereco
                };

                await _instituicao.Cadastrar(instituicao);

                return StatusCode(
                    201,
                    "Instituição cadastrada com sucesso " + instituicao.NomeFantasia
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
                var instituicaoBuscada = await _instituicao.BuscarPorId(id);

                if (instituicaoBuscada == null)
                {
                    return NotFound("Instituição não encontrada.");
                }

                return Ok(instituicaoBuscada);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _instituicao.Deletar(id);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(
    Guid id,
    [FromBody] Instituicao instituicao)
        {
            try
            {
                await _instituicao.Atualizar(id, instituicao);

                return Ok("Instituição atualizada com sucesso.");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}