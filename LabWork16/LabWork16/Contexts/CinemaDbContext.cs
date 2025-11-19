using AuthLib.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthLib.Contexts;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3114;Persist Security Info=True;User ID=ispp3114;Password=3114;Encrypt=True;Trust Server Certificate=True");
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ispp3114;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CinemaPrivilege>(entity =>
        {
            entity.HasKey(e => e.PrivilegeId);

            entity.ToTable("CinemaPrivilege");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasMany(d => d.Roles).WithMany(p => p.Privileges)
                .UsingEntity<Dictionary<string, object>>(
                    "CinemaRolePrivilege",
                    r => r.HasOne<CinemaUserRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CinemaRolePrivilege_CinemaUserRole"),
                    l => l.HasOne<CinemaPrivilege>().WithMany()
                        .HasForeignKey("PrivilegeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CinemaRolePrivilege_CinemaPrivilege"),
                    j =>
                    {
                        j.HasKey("PrivilegeId", "RoleId");
                        j.ToTable("CinemaRolePrivilege");
                    });
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
