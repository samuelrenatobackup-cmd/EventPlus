namespace EventPlusWebAPI.Utils
{
    public class CloudinarySettings
    {
        public string CloudName { get; set; } = string.Empty;

        //chave publica de identificacao API
        public string ApiKey { get; set; } = string.Empty;
        //chave secreta das requisicao
        public string ApiSecret {  get; set; } = string.Empty;
    }
}
