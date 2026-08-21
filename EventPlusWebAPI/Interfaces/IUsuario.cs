using EventPlusWebAPI.Models;

namespace EventPlusWebAPI.Interfaces
{
    public interface  IUsuario
    {
        Task Cadastrar(Usuario usuario);
        Task<List<Usuario>> Listar();
        Task<Usuario?> BuscarPorId(Guid id);
        Task Atualizar(Guid id, Usuario usuario);
        Task Deletar(Guid id);
    }
}

