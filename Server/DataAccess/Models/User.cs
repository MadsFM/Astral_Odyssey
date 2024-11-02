using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("users")]
[Index("Email", Name = "users_email_key", IsUnique = true)]
[Index("Username", Name = "users_username_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("userid")]
    public int Userid { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("passwordhash")]
    [StringLength(255)]
    public string Passwordhash { get; set; } = null!;

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Scoreboard> Scoreboards { get; set; } = new List<Scoreboard>();

    [InverseProperty("User")]
    public virtual ICollection<Userquestprogress> Userquestprogresses { get; set; } = new List<Userquestprogress>();

    [InverseProperty("User")]
    public virtual ICollection<Userrole> Userroles { get; set; } = new List<Userrole>();
}
