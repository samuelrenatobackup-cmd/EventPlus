using EventPlusWebAPI.BdContextEvent;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlusWebAPI.Repositories
{
    public class ComentarioRepository : IComentario
    {
        private readonly EventContext _context;

        public ComentarioRepository(EventContext comentario)
        {
            _context = comentario;
        }

        public async Task<List<Comentario>> Listar()
        {
            return await _context.Comentario.ToListAsync();
        }

       
        public async Task<Comentario> BuscarPorId(Guid id)
        {
            return await _context.Comentario.FindAsync(id);
        }

     
        public async Task Comentar(Comentario comentario)
        {
            comentario.IdComentario = Guid.NewGuid();

            await _context.Comentario.AddAsync(comentario);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Guid id, Comentario comentario)
        {
            var comentarioBuscado = await _context.Comentario.FindAsync(id);

            if (comentarioBuscado != null)
            {
                comentarioBuscado.DataComentario = comentario.DataComentario;
                comentarioBuscado.Descricao = comentario.Descricao;
                comentarioBuscado.IdEvento = comentario.IdEvento;
                comentarioBuscado.IdUsuario = comentario.IdUsuario;

                await _context.SaveChangesAsync();
            }
        }

        
        public async Task Deletar(Guid id)
        {
            var comentarioBuscado = await _context.Comentario.FindAsync(id);

            if (comentarioBuscado != null)
            {
                _context.Comentario.Remove(comentarioBuscado);

                await _context.SaveChangesAsync();
            }
        }
    }
}