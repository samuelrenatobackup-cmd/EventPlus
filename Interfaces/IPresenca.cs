using EventPlusWebAPI.Models;

namespace EventPlusWebAPI.Interfaces
{
    public interface IPresenca
    {
        Task Cadastrar(Presenca presenca);
        Task<List<Presenca>> Listar();
        Task Deletar(Guid id);
        Task Atualizar(Guid id, Presenca presenca);
        Task<Presenca> BuscarPorId(Guid id);
    }
}