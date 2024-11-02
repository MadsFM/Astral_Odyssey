using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[PrimaryKey("Userid", "Roleid")]
[Table("userroles")]
public partial class Userrole
{
    [Key]
    [Column("userid")]
    public int Userid { get; set; }

    [Key]
    [Column("roleid")]
    public int Roleid { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [ForeignKey("Roleid")]
    [InverseProperty("Userroles")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("Userid")]
    [InverseProperty("Userroles")]
    public virtual User User { get; set; } = null!;
}
