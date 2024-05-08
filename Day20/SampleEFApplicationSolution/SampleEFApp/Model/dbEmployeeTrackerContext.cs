using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SampleEFApp.Model
{
    public partial class dbEmployeeTrackerContext : DbContext
    {
        public dbEmployeeTrackerContext()
        {
        }

        public dbEmployeeTrackerContext(DbContextOptions<dbEmployeeTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeSkill> EmployeeSkills { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=823CBX3\\DEMOINSTANCE;Integrated Security=True;Initial Catalog=dbEmployeeTracker;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.Area1)
                    .HasName("pk_Area");

                entity.Property(e => e.Area1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Area");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EmployeeArea)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmployeeAreaNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeArea)
                    .HasConstraintName("fk_Area");
            });

            modelBuilder.Entity<EmployeeSkill>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.Skill })
                    .HasName("pk_employee_skill");

                entity.ToTable("EmployeeSkill");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.SkillLevel).HasColumnName("skillLevel");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeSkills)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_skill_eid");

                entity.HasOne(d => d.SkillNavigation)
                    .WithMany(p => p.EmployeeSkills)
                    .HasForeignKey(d => d.Skill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Skill_EmplSkill");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.SkillId).HasColumnName("Skill_id");

                entity.Property(e => e.Skill1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Skill");

                entity.Property(e => e.SkillDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
