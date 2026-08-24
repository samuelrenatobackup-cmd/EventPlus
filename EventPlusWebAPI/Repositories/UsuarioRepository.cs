using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.BdContextEvent;
using EventPlusWebAPI.DTO;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using EventPlusWebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlusWebAPI.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly EventContext _context;

        public UsuarioRepository(EventContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, Usuario usuario)
        {
            var usuarioBuscado = await _context.Usuario.FindAsync(id);

            if (usuarioBuscado != null)
            {
                usuarioBuscado.Nome = usuario.Nome;
                usuarioBuscado.Email = usuario.Email;

                if (!string.IsNullOrEmpty(usuario.Senha))
                {
                    usuarioBuscado.Senha = Criptografia.GerarHash(usuario.Senha);
                }

                usuarioBuscado.IdTipoUsuario = usuario.IdTipoUsuario;

                await _context.SaveChangesAsync();
            }
        }
        public async Task<Usuario> BuscarPorId(Guid id)
        {
            return await _context.Usuario.FirstOrDefaultAsync(t => t.IdUsuario == id);
        }

        public async Task Cadastrar(UsuarioDTO dto)
        {
            Usuario usuario = new Usuario
            {
                IdUsuario = Guid.NewGuid(),
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = Criptografia.GerarHash(dto.Senha),
                IdTipoUsuario = dto.IdTipoUsuario
            };

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> Listar()
        {
            return await _context.Usuario.Include(u => u.IdTipoUsuarioNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Deletar(Guid id)
        {
            var usuarioBuscado = await _context.Usuario.FindAsync(id);

            if (usuarioBuscado != null)
            {
                _context.Usuario.Remove(usuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<Usuario?> BuscarPorEmailESenha(
            string email,
            string senha)
        {
            var usuario = await _context.Usuario
                .Include(u => u.IdTipoUsuarioNavigation)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return null;
            }

            bool senhaValida = Criptografia.CompararHash(
                senha,
                usuario.Senha
            );

            if (!senhaValida)
            {
                return null;
            }

            return usuario;
        }
    }
}