using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ManagerEmployees.Models
{
    public partial class testemployeeContext : DbContext
    {
        public testemployeeContext()
        {
        }

        public testemployeeContext(DbContextOptions<testemployeeContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;

        public bool EmployeeExists(string rfc){
            return Employees.Any(e => e.Rfc == rfc);
        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.Rfc, "UQ__Employee__CAFFA85EF3D09213")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BornDate).HasColumnType("date");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Rfc)
                    .HasMaxLength(13)
                    .HasColumnName("RFC");

                entity.HasOne(d => d.oStatus)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Status");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PK__Status__B450643A02E4766D");

                entity.ToTable("Status");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
