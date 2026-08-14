using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlusWebAPI.Models;

[Index("Email", Name = "UQ_Usuario_Email", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public Guid IdUsuario { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(256)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string Senha { get; set; } = null!;

    public Guid? IdTipoUsuario { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Comentario> Comentario { get; set; } = new List<Comentario>();

    [ForeignKey("IdTipoUsuario")]
    [InverseProperty("Usuario")]
    public virtual TipoUsuario? IdTipoUsuarioNavigation { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Presenca> Presenca { get; set; } = new List<Presenca>();
}
