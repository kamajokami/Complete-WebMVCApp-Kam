using Microsoft.EntityFrameworkCore;

namespace WebMVCApplKamPublic.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source=web_mvc_app_db.sqlite");
        }
    }
}
