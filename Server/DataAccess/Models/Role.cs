using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("roles")]
[Index("Name", Name = "roles_name_key", IsUnique = true)]
[Index("Normalizedname", Name = "roles_normalizedname_key", IsUnique = true)]
public partial class Role
{
    [Key]
    [Column("roleid")]
    public int Roleid { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("normalizedname")]
    [StringLength(50)]
    public string Normalizedname { get; set; } = null!;

    [ForeignKey("Roleid")]
    [InverseProperty("Roles")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
