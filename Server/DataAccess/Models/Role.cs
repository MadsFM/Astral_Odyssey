using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("roles")]
[Index("Rolename", Name = "roles_rolename_key", IsUnique = true)]
public partial class Role
{
    [Key]
    [Column("roleid")]
    public int Roleid { get; set; }

    [Column("rolename")]
    [StringLength(50)]
    public string Rolename { get; set; } = null!;

    [ForeignKey("Roleid")]
    [InverseProperty("Roles")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
