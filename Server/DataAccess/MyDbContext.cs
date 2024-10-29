using System;
using System.Collections.Generic;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Planet> Planets { get; set; }

    public virtual DbSet<Quest> Quests { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<Scoreboard> Scoreboards { get; set; }

    public virtual DbSet<Universe> Universes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userquestprogress> Userquestprogresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("user_role", new[] { "Admin", "Player", "GameManager" });

        modelBuilder.Entity<Planet>(entity =>
        {
            entity.HasKey(e => e.Planetid).HasName("planets_pkey");

            entity.Property(e => e.Isdiscovered).HasDefaultValue(false);

            entity.HasOne(d => d.Universe).WithMany(p => p.Planets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("planets_universeid_fkey");
        });

        modelBuilder.Entity<Quest>(entity =>
        {
            entity.HasKey(e => e.Questid).HasName("quests_pkey");

            entity.Property(e => e.Iscompleted).HasDefaultValue(false);

            entity.HasOne(d => d.Planet).WithMany(p => p.Quests).HasConstraintName("quests_planetid_fkey");

            entity.HasOne(d => d.Universe).WithMany(p => p.Quests).HasConstraintName("quests_universeid_fkey");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Quizid).HasName("quizzes_pkey");

            entity.HasOne(d => d.Quest).WithMany(p => p.Quizzes).HasConstraintName("quizzes_questid_fkey");
        });

        modelBuilder.Entity<Scoreboard>(entity =>
        {
            entity.HasKey(e => e.Scoreid).HasName("scoreboard_pkey");

            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.User).WithMany(p => p.Scoreboards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("scoreboard_userid_fkey");
        });

        modelBuilder.Entity<Universe>(entity =>
        {
            entity.HasKey(e => e.Universeid).HasName("universes_pkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Userquestprogress>(entity =>
        {
            entity.HasKey(e => e.Progressid).HasName("userquestprogress_pkey");

            entity.Property(e => e.Iscompleted).HasDefaultValue(false);
            entity.Property(e => e.Lastupdated).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Quest).WithMany(p => p.Userquestprogresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userquestprogress_questid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Userquestprogresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userquestprogress_userid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
