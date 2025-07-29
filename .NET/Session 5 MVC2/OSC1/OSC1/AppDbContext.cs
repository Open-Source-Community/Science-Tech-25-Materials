using Microsoft.EntityFrameworkCore;
using OSC1.Models;

namespace OSC1
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=MVC;Integrated Security=True");
           
        }
        public DbSet<Employee> Employees { get; set; }

    }
}
