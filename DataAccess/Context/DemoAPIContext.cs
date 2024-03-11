using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class DemoAPIContext : DbContext
    {
        public DemoAPIContext(DbContextOptions<DemoAPIContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<Employee>(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithOne(p => p.Employee)
                .HasForeignKey(p => p.EmployeeId);
        }
    }
}
