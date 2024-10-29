using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models;

[Table("users")]
[Index("Username", Name = "users_username_key", IsUnique = true)]
public partial class User : IdentityUser<int>
{
  
    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Scoreboard> Scoreboards { get; set; } = new List<Scoreboard>();

    [InverseProperty("User")]
    public virtual ICollection<Userquestprogress> Userquestprogresses { get; set; } = new List<Userquestprogress>();
}
