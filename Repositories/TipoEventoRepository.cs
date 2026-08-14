using EventPlusWebAPI.BdContextEvent;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlusWebAPI.Repositories
{
    public class TipoEventoRepository : ITipoEvento
    {
        private readonly EventContext _tipoEvento;

        public TipoEventoRepository(EventContext context)
        {
            _tipoEvento = context;
        }

        public async Task<List<TipoEvento>> Listar()
        {
            return await _tipoEvento.TipoEvento
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Cadastrar(TipoEvento tipoEvento)
        {
            await _tipoEvento.TipoEvento.AddAsync(tipoEvento);
            await _tipoEvento.SaveChangesAsync();
        }

        public async Task<TipoEvento?> BuscarPorId(Guid id)
        {
            return await _tipoEvento.TipoEvento
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdTipoEvento == id);
        }

        public async Task Atualizar(Guid id, TipoEvento tipoEvento)
        {
            var tipoEventoBanco = await _tipoEvento.TipoEvento
                .FirstOrDefaultAsync(t => t.IdTipoEvento == id);

            if (tipoEventoBanco == null)
            {
                throw new Exception("Tipo de evento não encontrado.");
            }

            tipoEventoBanco.TituloTipoEvento = tipoEvento.TituloTipoEvento;

            await _tipoEvento.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var tipoEvento = await _tipoEvento.TipoEvento
                .FirstOrDefaultAsync(x => x.IdTipoEvento == id);

            if (tipoEvento == null)
            {
                throw new Exception("Tipo de evento não encontrado.");
            }

            _tipoEvento.TipoEvento.Remove(tipoEvento);
            await _tipoEvento.SaveChangesAsync();
        }
    }
}

