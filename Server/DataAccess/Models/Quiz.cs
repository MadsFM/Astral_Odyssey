using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("quizzes")]
public partial class Quiz
{
    [Key]
    [Column("quizid")]
    public int Quizid { get; set; }

    [Column("question")]
    public string Question { get; set; } = null!;

    [Column("answer")]
    [StringLength(100)]
    public string Answer { get; set; } = null!;

    [Column("hint")]
    public string? Hint { get; set; }

    [Column("questid")]
    public int? Questid { get; set; }

    [ForeignKey("Questid")]
    [InverseProperty("Quizzes")]
    public virtual Quest? Quest { get; set; }
}
