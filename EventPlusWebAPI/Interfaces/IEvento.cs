using EventPlusWebAPI.Models;

namespace EventPlusWebAPI.Interfaces
{
    public interface IEvento
    {

        Task Cadastrar(Evento evento);
        Task<List<Evento>> Listar();
        Task Deletar(Guid id);
        Task Atualizar(Guid id, Evento evento);
        Task<Evento> BuscarPorId(Guid id);
        
    }
}

