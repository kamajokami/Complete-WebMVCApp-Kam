using Microsoft.EntityFrameworkCore;
using WebMVCApplKamPublic.Models;

namespace WebMVCApplKamPublic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
        }
        public DbSet<Person> Persons { get; set; }

    }
}
