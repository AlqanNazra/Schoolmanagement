using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Modules.Students.Entities;
using SchoolManagementSystem.Modules.Teachers.Entities;
using SchoolManagementSystem.Modules.Classes.Entities;
using SchoolManagementSystem.Modules.Enrollments.Entities;

namespace SchoolManagementSystem.Configurations.AppDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Murid> Students { get; set; }
        public DbSet<Guru> Teachers { get; set; }
        public DbSet<Kelas> Classes { get; set; }
        public DbSet<Pendaftaran> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary Key Setiap Entitas
            // Konfigurasi entitas Murid
            modelBuilder.Entity<Murid>()
                .HasKey(s => s.id_student);

            modelBuilder.Entity<Murid>()
                .Property(s => s.id_student)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Murid>()
                .HasIndex(s => s.email)
                .IsUnique();

            // Konfigurasi entitas Guru
            modelBuilder.Entity<Guru>()
                .HasKey(t => t.id_teacher);

            modelBuilder.Entity<Guru>()
                .HasIndex(t => t.email_teacher)
                .IsUnique();

            // Konfigurasi entitas Kelas
            modelBuilder.Entity<Kelas>()
                .HasKey(c => c.id_kelas);

            // Konfigurasi entitas Pendaftaran
            modelBuilder.Entity<Pendaftaran>()
                .HasKey(e => e.id_enrollment);

            modelBuilder.Entity<Pendaftaran>()
                .HasIndex(e => new { e.id_student, e.id_kelas })
                .IsUnique();

            // Relasi antara Entitas
        modelBuilder.Entity<Kelas>()
            .HasOne(k => k.GuruUtama)
            .WithMany(g => g.KelasUtama)
            .HasForeignKey(k => k.id_guru)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Kelas>()
            .HasMany(k => k.Pengajar)
            .WithMany(g => g.KelasDiajarkan)
            .UsingEntity(j => j.ToTable("KelasGuru"));

            modelBuilder.Entity<Pendaftaran>()
                .HasOne(e => e.Murid)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.id_student)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pendaftaran>()
                .HasOne(e => e.kelas)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.id_kelas)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}