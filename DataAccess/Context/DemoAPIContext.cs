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

    }
}
