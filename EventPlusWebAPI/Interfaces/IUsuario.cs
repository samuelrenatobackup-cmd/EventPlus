
using EventPlusWebAPI.DTO;
using EventPlusWebAPI.Models;

namespace EventPlusWebAPI.Interfaces
{
    public interface IUsuario
    {
        Task Cadastrar(UsuarioDTO usuario);

        Task Atualizar(Guid id, Usuario usuario);

        Task Deletar(Guid id);

        Task<List<Usuario>> Listar();

        Task<Usuario?> BuscarPorId(Guid id);

        Task<Usuario> BuscarPorEmailESenha(string email, string senha);

    }
}