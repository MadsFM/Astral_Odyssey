using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("scoreboard")]
public partial class Scoreboard
{
    [Key]
    [Column("scoreid")]
    public int Scoreid { get; set; }

    [Column("userid")]
    public int Userid { get; set; }

    [Column("points")]
    public int Points { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Scoreboards")]
    public virtual User User { get; set; } = null!;
}
