using EventPlusWebAPI.BdContextEvent;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Runtime.InteropServices;

namespace EventPlusWebAPI.Repositories
{
    public class InstituicaoRepository : IInstituicao
    {
        private readonly EventContext _instituicao;

        public InstituicaoRepository(EventContext instituicao)
        {
            _instituicao = instituicao;
        }

        public async Task<List<Instituicao>> Listar()
        {
            return await _instituicao.Instituicao
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Instituicao> BuscarPorId(Guid id)
        {
            return await _instituicao.Instituicao.FirstOrDefaultAsync(i => i.IdInstituicao == id);
        }
        public async Task Cadastrar(Instituicao instituicao)
        {
            await _instituicao.AddAsync(instituicao);
            await _instituicao.SaveChangesAsync();
        }
        public async Task Atualizar(Guid id, Instituicao instituicao)
        {
            var instituicaoBuscada = await _instituicao.Instituicao.FindAsync(id);

            if (instituicaoBuscada != null)
            {
                instituicaoBuscada.Cnpj = instituicao.Cnpj;
                instituicaoBuscada.NomeFantasia = instituicao.NomeFantasia;
                instituicaoBuscada.Endereco = instituicao.Endereco;

                await _instituicao.SaveChangesAsync();
            }
        }
        public async Task Deletar(Guid id)
        {
            var instituicaoBuscada = await _instituicao.Instituicao.FindAsync(id);
            if(instituicaoBuscada != null)
            {
                _instituicao.Instituicao.Remove(instituicaoBuscada);
                await _instituicao.SaveChangesAsync();
            }
        }
    }
}