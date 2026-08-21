using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlusWebAPI.Models;

public partial class Presenca
{
    [Key]
    public Guid IdPresenca { get; set; }

    public bool Situacao { get; set; }

    public Guid IdEvento { get; set; }

    public Guid? IdUsuario { get; set; }

    [ForeignKey("IdEvento")]
    [InverseProperty("Presenca")]
    public virtual Evento IdEventoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Presenca")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
