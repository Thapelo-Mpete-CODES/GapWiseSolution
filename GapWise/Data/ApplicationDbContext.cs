using GapWise.Models;
using Microsoft.EntityFrameworkCore;

namespace GapWise.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Concept> Concepts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<StudentTest> StudentTests { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // StudentAnswer → Question (Cascade is safe)
            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.Question)
                .WithMany(q => q.StudentAnswers)
                .HasForeignKey(sa => sa.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // StudentAnswer → StudentTest (Restrict to prevent multiple cascade paths)
            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.StudentTest)
                .WithMany(st => st.StudentAnswers)
                .HasForeignKey(sa => sa.StudentTestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}