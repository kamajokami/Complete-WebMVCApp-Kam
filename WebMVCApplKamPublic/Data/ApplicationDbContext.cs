using Microsoft.EntityFrameworkCore;
using WebMVCApplKamPublic.Models;

namespace WebMVCApplKamPublic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source=web_mvc_app_db.sqlite");
        }
    }
}
