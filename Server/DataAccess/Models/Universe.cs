using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("universes")]
public partial class Universe
{
    [Key]
    [Column("universeid")]
    public int Universeid { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty("Universe")]
    public virtual ICollection<Planet> Planets { get; set; } = new List<Planet>();

    [InverseProperty("Universe")]
    public virtual ICollection<Quest> Quests { get; set; } = new List<Quest>();
}
