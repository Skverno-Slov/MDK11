using System;
using System.Collections.Generic;
using LabWork16.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork16.Contexts;

public partial class CinemaDbContext : DbContext
{
    public CinemaDbContext()
    {
    }

    public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CinemaPrivilege> CinemaPrivileges { get; set; }

    public virtual DbSet<CinemaUser> CinemaUsers { get; set; }

    public virtual DbSet<CinemaUserRole> CinemaUserRoles { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ispp3114;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CinemaPrivilege>(entity =>
        {
            entity.HasKey(e => e.PrivilegeId);

            entity.ToTable("CinemaPrivilege");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<CinemaUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("CinemaUser");

            entity.Property(e => e.HashPassword).HasMaxLength(200);
            entity.Property(e => e.Login).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.CinemaUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CinemaUser_CinemaUserRole");
        });

        modelBuilder.Entity<CinemaUserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("CinemaUserRole");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("Movie");

            entity.Property(e => e.AgeRating)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Duration).HasDefaultValue((short)90);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Year).HasDefaultValueSql("(datepart(year,getdate()))");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
