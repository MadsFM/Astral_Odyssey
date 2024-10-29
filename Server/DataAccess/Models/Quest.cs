using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("quests")]
public partial class Quest
{
    [Key]
    [Column("questid")]
    public int Questid { get; set; }

    [Column("title")]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("planetid")]
    public int? Planetid { get; set; }

    [Column("universeid")]
    public int? Universeid { get; set; }

    [Column("iscompleted")]
    public bool? Iscompleted { get; set; }

    [ForeignKey("Planetid")]
    [InverseProperty("Quests")]
    public virtual Planet? Planet { get; set; }

    [InverseProperty("Quest")]
    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    [ForeignKey("Universeid")]
    [InverseProperty("Quests")]
    public virtual Universe? Universe { get; set; }

    [InverseProperty("Quest")]
    public virtual ICollection<Userquestprogress> Userquestprogresses { get; set; } = new List<Userquestprogress>();
}
