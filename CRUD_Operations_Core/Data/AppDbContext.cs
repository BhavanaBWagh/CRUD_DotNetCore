using CRUD_Operations_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations_Core.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Person> Person { get; set; }
    }
}
