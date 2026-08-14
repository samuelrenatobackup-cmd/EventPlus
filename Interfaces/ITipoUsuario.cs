using EventPlusWebAPI.Models;

namespace EventPlusWebAPI.Interfaces
{
    public interface ITipoUsuario
    {
        Task Cadastrar(TipoUsuario tipoUsuario);
        Task<List<TipoUsuario>> Listar();
        Task<TipoUsuario?> BuscarPorId(Guid id);
        Task Atualizar(Guid id, TipoUsuario tipoUsuario);
        Task Deletar(Guid id);
       
    }
}

