using Microsoft.EntityFrameworkCore;
using System.Xml;
using WebMVCApplKamPublic.Models;
using WebMVCApplKamPublic.Models.ViewModels;

namespace WebMVCApplKamPublic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
        }
        public DbSet<Person> Persons { get; set; }

        public DbSet<PriceViewModel> ComodityPrices { get; set; }

        public DbSet<CheckboxOption> CheckBoxOptions { get; set; }

        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}
