namespace EventPlusWebAPI.DTO
{
    public class InstituicaoDTO
    { 

        public Guid IdInstituicao { get; }

        public string CNPJ { get; set; }
        public string NomeFantasia { get; set; }
        public string Endereco { get; set; }
        

    }
}