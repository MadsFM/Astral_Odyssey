using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("users")]
[Index("Normalizedusername", Name = "users_normalizedusername_key", IsUnique = true)]
[Index("Username", Name = "users_username_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("userid")]
    public int Userid { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("normalizedusername")]
    [StringLength(50)]
    public string Normalizedusername { get; set; } = null!;

    [Column("email")]
    [StringLength(256)]
    public string? Email { get; set; }

    [Column("normalizedemail")]
    [StringLength(256)]
    public string? Normalizedemail { get; set; }

    [Column("emailconfirmed")]
    public bool? Emailconfirmed { get; set; }

    [Column("passwordhash")]
    [StringLength(255)]
    public string Passwordhash { get; set; } = null!;

    [Column("securitystamp")]
    [StringLength(36)]
    public string? Securitystamp { get; set; }

    [Column("concurrencystamp")]
    [StringLength(36)]
    public string? Concurrencystamp { get; set; }

    [Column("phonenumber")]
    [StringLength(15)]
    public string? Phonenumber { get; set; }

    [Column("phonenumberconfirmed")]
    public bool? Phonenumberconfirmed { get; set; }

    [Column("twofactorenabled")]
    public bool? Twofactorenabled { get; set; }

    [Column("lockoutend", TypeName = "timestamp without time zone")]
    public DateTime? Lockoutend { get; set; }

    [Column("lockoutenabled")]
    public bool? Lockoutenabled { get; set; }

    [Column("accessfailedcount")]
    public int? Accessfailedcount { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Scoreboard> Scoreboards { get; set; } = new List<Scoreboard>();

    [InverseProperty("User")]
    public virtual ICollection<Userquestprogress> Userquestprogresses { get; set; } = new List<Userquestprogress>();

    [ForeignKey("Userid")]
    [InverseProperty("Users")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
