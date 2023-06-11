using BAPMap.Model;
using Microsoft.EntityFrameworkCore;
namespace BAPMap
{
    public class Context : DbContext
    {
        public DbSet<City> Cities { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Repository.GetConnectionString());
        }
    }
}