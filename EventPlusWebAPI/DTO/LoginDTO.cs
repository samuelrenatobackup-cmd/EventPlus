using System.ComponentModel.DataAnnotations;

namespace EventPlusWebAPI.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O e-mail é Obrigadoria para autenticação")]
        [EmailAddress(ErrorMessage = "Informa um e-mail valido")]

        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A semha é obrigadoria para autenticação")]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 a60 caracteres")]

        public string Senha { get; set; } = string.Empty;
    }
}
