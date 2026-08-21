using EventPlusWebAPI.Models;

namespace EventPlusWebAPI.Interfaces
{
    public interface IComentario
    {
        Task Comentar(Comentario comentario);
        Task<List<Comentario>> Listar();
        Task Deletar(Guid id);
        Task Atualizar(Guid id, Comentario comentario);
        Task<Comentario> BuscarPorId(Guid id);
    }
}