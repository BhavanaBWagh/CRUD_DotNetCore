using CRUD_Operations_Core.Models;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

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
        public virtual DbSet<PersonDetail> PersonDetails { get; set; }

        public virtual DbSet<Student> Students { get; set; }
    }
}
