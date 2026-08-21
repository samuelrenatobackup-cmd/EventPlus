using EventPlusWebAPI.BdContextEvent;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace EventPlusWebAPI.Repositories
{
    public class EventoRepository : IEvento
    {
        private readonly EventContext _context;

        public EventoRepository(EventContext instituicao)
        {
            _context = instituicao;
        }

        public async Task<List<Evento>> Listar()
        {
            return await _context.Evento.ToListAsync();
        }
        public async Task<Evento> BuscarPorId(Guid id)
        {
            return await _context.Evento.FirstOrDefaultAsync(i => i.IdEvento == id);
        }
        public async Task Cadastrar(Evento evento)
        {
            await _context.AddAsync(evento);
            await _context.SaveChangesAsync();
        }
        
            public async Task Atualizar(Guid id, Evento evento)
        {
            var eventoBuscado = await _context.Evento.FindAsync(id);

            if (eventoBuscado != null)
            {
                eventoBuscado.NomeEvento = evento.NomeEvento;
                eventoBuscado.DataEvento = evento.DataEvento;
                eventoBuscado.Descricao = evento.Descricao;
                eventoBuscado.ImagemUrl = evento.ImagemUrl;
                eventoBuscado.IdTipoEvento = evento.IdTipoEvento;
                eventoBuscado.IdInstituicao = evento.IdInstituicao;

                await _context.SaveChangesAsync();
            }
        }
        
        public async Task Deletar(Guid id)
        {
            var eventoBuscado = await _context.Evento.FindAsync(id);


            if (eventoBuscado != null)
                {
                    _context.Evento.Remove(eventoBuscado);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
