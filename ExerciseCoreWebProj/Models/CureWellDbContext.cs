using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExerciseCoreWebProj.Models;

public partial class CureWellDbContext : DbContext
{
    public CureWellDbContext()
    {
    }

    public CureWellDbContext(DbContextOptions<CureWellDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Surgery> Surgeries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=INL669;database=CureWellDb;trusted_connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__2DC00EBF20086E63");

            entity.ToTable("Doctor");

            entity.Property(e => e.DoctorName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DoctorSpecialization>(entity =>
        {
            entity.HasKey(e => new { e.DoctorId, e.SpecializationCode }).HasName("pk");

            entity.ToTable("DoctorSpecialization");

            entity.Property(e => e.SpecializationCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SpecializationDate).HasColumnType("date");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorSpecializations)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorSpe__Docto__286302EC");

            entity.HasOne(d => d.SpecializationCodeNavigation).WithMany(p => p.DoctorSpecializations)
                .HasForeignKey(d => d.SpecializationCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorSpe__Speci__29572725");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationCode).HasName("PK__Speciali__E2F4CF4E82D163FA");

            entity.ToTable("Specialization");

            entity.Property(e => e.SpecializationCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SpecializationName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Surgery>(entity =>
        {
            entity.HasKey(e => e.SurgeryId).HasName("PK__Surgery__08AD55FDE7449465");

            entity.ToTable("Surgery");

            entity.Property(e => e.EndTime).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.StartTime).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.SurgeryCategory)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SurgeryDate).HasColumnType("date");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Surgeries)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Surgery__DoctorI__2D27B809");

            entity.HasOne(d => d.SurgeryCategoryNavigation).WithMany(p => p.Surgeries)
                .HasForeignKey(d => d.SurgeryCategory)
                .HasConstraintName("FK__Surgery__Surgery__2E1BDC42");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
