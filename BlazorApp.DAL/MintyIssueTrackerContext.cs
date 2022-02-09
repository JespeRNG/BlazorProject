using System.Reflection;
using BlazorApp.DAL.Seeder;
using BlazorApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DAL
{
    public class MintyIssueTrackerContext : DbContext
    {
        public MintyIssueTrackerContext(string connectionString) : base(GetOptions(connectionString)) { }
        public MintyIssueTrackerContext(DbContextOptions<MintyIssueTrackerContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Roles { get; set; }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            DatabaseSeeder.SeedDataBase(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
