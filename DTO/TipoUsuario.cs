
using System.ComponentModel.DataAnnotations;

namespace EventPlusWebAPI.DTO;

/// <summary>
/// Data Transfer Object (DTO) para cadastro e atualização do Perfil/Tipo de Usuário.
/// </summary>
public class TipoUsuarioDTO
{
    /// <summary>
    /// Título do tipo de usuário
    /// </summary>
    [Required(ErrorMessage = "O titulo é obrigatório.")]
    [StringLength(100, ErrorMessage = "O título pode ter no máximo 100 caracteres.")]
    public string Titulo { get; set; } = string.Empty;
}
