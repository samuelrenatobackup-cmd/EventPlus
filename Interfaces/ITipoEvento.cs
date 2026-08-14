using EventPlusWebAPI.Models;

namespace EventPlusWebAPI.Interfaces
{
    public interface ITipoEvento
    {
        Task Cadastrar(TipoEvento tipoEvento);
        Task<List<TipoEvento>> Listar();
        Task<TipoEvento?> BuscarPorId(Guid id);
        Task Atualizar(Guid id, TipoEvento tipoEvento);
        Task Deletar(Guid id);

    }
}

