using GreenSchoolCAT.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenSchoolCAT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ტესტები
            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(t => t.GuidId);
                entity.Property<int>("Id").ValueGeneratedOnAdd();

                entity.HasOne(t => t.Teacher)
                    .WithMany(u => u.TestsCreated)
                    .HasForeignKey(t => t.TeacherId);
            });

            // ტესტის კითხვები (სხვადასხვა ადგილას როა)
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasOne(q => q.Test)
                    .WithMany(t => t.Questions)
                    .HasForeignKey(q => q.TestId);
            });

            // შედეგები
            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.HasKey(tr => tr.Id);

                entity.HasOne(tr => tr.Test)
                    .WithMany(t => t.Results)
                    .HasForeignKey(tr => tr.TestId);

                entity.HasOne(tr => tr.Student)
                    .WithMany(u => u.ResultsTaken)
                    .HasForeignKey(tr => tr.StudentId);
            });

            // უსერი
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.TestsCreated)
                    .WithOne(t => t.Teacher)
                    .HasForeignKey(t => t.TeacherId);

                entity.HasMany(u => u.ResultsTaken)
                    .WithOne(r => r.Student)
                    .HasForeignKey(r => r.StudentId);
            });
        }
    }
}