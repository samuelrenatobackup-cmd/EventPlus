namespace EventPlusWebAPI.DTO
{
    public class ComentarioDTO
    {
        public DateTime DataComentario { get; set; }
        public string Descricao { get; set; }
        public Guid IdEvento { get; set; }
        public Guid IdUsuario { get; set; }
    }
}