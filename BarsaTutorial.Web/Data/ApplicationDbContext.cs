using BarsaTutorial.Web.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BarsaTutorial.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<FileType> FileTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Education>().HasKey(e => e.ID);
            modelBuilder.Entity<Education>().Property(e => e.Title).HasMaxLength(100).IsUnicode();
            modelBuilder.Entity<Education>().HasOne(e => e.Category)
                .WithMany(e => e.Educations)
                .HasForeignKey(e => e.CategoryID);
            modelBuilder.Entity<Education>().HasOne(e => e.FileType)
                .WithMany(e => e.Educations)
                .HasForeignKey(e => e.FileTypeID);
            modelBuilder.Entity<Education>().ToTable("Educations");

            modelBuilder.Entity<Lesson>().HasKey(e => e.ID);
            modelBuilder.Entity<Lesson>().Property(e => e.Title).HasMaxLength(100).IsUnicode();
            modelBuilder.Entity<Lesson>().Property(e => e.FileAddress).HasMaxLength(100).IsUnicode();
            modelBuilder.Entity<Lesson>().HasOne(e => e.Education)
                .WithMany(e => e.Lessons)
                .HasForeignKey(e => e.EducationID);
            modelBuilder.Entity<Lesson>().ToTable("Lessons");

            modelBuilder.Entity<FileType>().HasKey(e => e.ID);
            modelBuilder.Entity<FileType>().Property(e => e.Title).HasMaxLength(100).IsUnicode();
            modelBuilder.Entity<FileType>().ToTable("FileTypes");

            modelBuilder.Entity<Category>().HasKey(e => e.ID);
            modelBuilder.Entity<Category>().Property(e => e.Title).HasMaxLength(100).IsUnicode();
            modelBuilder.Entity<Category>().ToTable("Categories");
        }
    }
}
