namespace EventPlusWebAPI.DTO
{
    public class PresencaDTO
    {
        public bool Situacao { get; set; }

        public Guid IdEvento { get; set; }

        public Guid? IdUsuario { get; set; }
    }
}