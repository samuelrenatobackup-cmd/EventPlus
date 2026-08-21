using EventPlusWebAPI.BdContextEvent;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlusWebAPI.Repositories
{
    public class PresencaRepository : IPresenca
    {
        private readonly EventContext _context;

        public PresencaRepository(EventContext presenca)
        {
            _context = presenca;
        }

        public async Task<List<Presenca>> Listar()
        {
            return await _context.Presenca
                .Include(p => p.IdEventoNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .ToListAsync();
        }

        public async Task<Presenca> BuscarPorId(Guid id)
        {
            return await _context.Presenca
                .Include(p => p.IdEventoNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefaultAsync(p => p.IdPresenca == id);
        }

        public async Task Cadastrar(Presenca presenca)
        {
            await _context.Presenca.AddAsync(presenca);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Guid id, Presenca presenca)
        {
            var presencaBuscada = await _context.Presenca.FindAsync(id);

            if (presencaBuscada != null)
            {
                presencaBuscada.Situacao = presenca.Situacao;
                presencaBuscada.IdEvento = presenca.IdEvento;
                presencaBuscada.IdUsuario = presenca.IdUsuario;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Deletar(Guid id)
        {
            var presencaBuscada = await _context.Presenca.FindAsync(id);

            if (presencaBuscada != null)
            {
                _context.Presenca.Remove(presencaBuscada);
                await _context.SaveChangesAsync();
            }
        }
    }
}