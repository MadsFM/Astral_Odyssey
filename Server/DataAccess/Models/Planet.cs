using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("planets")]
public partial class Planet
{
    [Key]
    [Column("planetid")]
    public int Planetid { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("universeid")]
    public int Universeid { get; set; }

    [Column("isdiscovered")]
    public bool? Isdiscovered { get; set; }

    [InverseProperty("Planet")]
    public virtual ICollection<Quest> Quests { get; set; } = new List<Quest>();

    [ForeignKey("Universeid")]
    [InverseProperty("Planets")]
    public virtual Universe Universe { get; set; } = null!;
}
