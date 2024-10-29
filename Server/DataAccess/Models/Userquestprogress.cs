using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("userquestprogress")]
public partial class Userquestprogress
{
    [Key]
    [Column("progressid")]
    public int Progressid { get; set; }

    [Column("userid")]
    public int Userid { get; set; }

    [Column("questid")]
    public int Questid { get; set; }

    [Column("iscompleted")]
    public bool? Iscompleted { get; set; }

    [Column("lastupdated", TypeName = "timestamp without time zone")]
    public DateTime? Lastupdated { get; set; }

    [ForeignKey("Questid")]
    [InverseProperty("Userquestprogresses")]
    public virtual Quest Quest { get; set; } = null!;

    [ForeignKey("Userid")]
    [InverseProperty("Userquestprogresses")]
    public virtual User User { get; set; } = null!;
}
