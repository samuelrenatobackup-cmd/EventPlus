namespace EventPlusWebAPI.DTO
{
    public class EventoDTO
    {
        public Guid IdEvento { get; set; }
        public DateTime DataEvento
        { get; set; }
        public string NomeEvento {  get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public Guid IdTipoEvento { get; set; }
        public Guid IdInstituicao { get; set; }

    }
}