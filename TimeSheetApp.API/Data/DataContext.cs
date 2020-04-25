using TimeSheetApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace TimeSheetApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        public DbSet<User> Users { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Day>().HasKey(e => new { e.ReportedDate, e.UserId, e.DaySequenceId });

            modelBuilder.Entity<UserProject>().HasKey(sc => new { sc.UserId, sc.ProjectId });

            modelBuilder.Entity<UserProject>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.UserProjects)
                .HasForeignKey(sc => sc.UserId);


            modelBuilder.Entity<UserProject>()
                .HasOne<Project>(sc => sc.Project)
                .WithMany(s => s.UserProjects)
                .HasForeignKey(sc => sc.ProjectId);
        }
    }
}