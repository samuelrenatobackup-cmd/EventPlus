using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlusWebAPI.Models;

public partial class Comentario
{
    [Key]
    public Guid IdComentario { get; set; }

    public DateTime DataComentario { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Descricao { get; set; } = null!;

    public bool Exibe { get; set; }

    public Guid IdEvento { get; set; }

    public Guid? IdUsuario { get; set; }

    [ForeignKey("IdEvento")]
    [InverseProperty("Comentario")]
    public virtual Evento IdEventoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Comentario")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
