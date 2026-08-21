using EventPlusWebAPI.BdContextEvent;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;

namespace EventPlusWebAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        private readonly EventContext _context;

        public TipoUsuarioRepository(EventContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, TipoUsuario TipoUsuario)
        {
            var tipoUsuarioBuscado = _context.TipoUsuario.Find(id);
            if (tipoUsuarioBuscado != null)
            {
                tipoUsuarioBuscado.TituloTipoUsuario = TipoUsuario.TituloTipoUsuario;
                _context.TipoUsuario.Update(tipoUsuarioBuscado);
                await _context.SaveChangesAsync();

            }
        }
        public async Task<TipoUsuario> BuscarPorId(Guid id)
        {
            return await _context.TipoUsuario.FirstOrDefaultAsync(t => t.IdTipoUsuario == id);
        }

        public async Task Cadastrar(TipoUsuario tipoUsuario)
        {
            await _context.TipoUsuario.AddAsync(tipoUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TipoUsuario>> Listar()
        {

            return await _context.TipoUsuario.AsNoTracking().ToListAsync();
        }
        public async Task Deletar(Guid id)
        {
            var tipoUsuarioBuscado = await _context.TipoUsuario.FindAsync(id);
            if (tipoUsuarioBuscado != null)
            { 
                _context.TipoUsuario.Remove(tipoUsuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }

       
    }
}