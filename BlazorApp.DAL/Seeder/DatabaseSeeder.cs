using BlazorApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DAL.Seeder
{
    public static class DatabaseSeeder
    {
        public static void SeedDataBase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                    new Role() { Id = 1, Name = "Admin" },
                    new Role() { Id = 2, Name = "Staff" },
                    new Role() { Id = 3, Name = "Customer" }
                );
        }
    }
}
