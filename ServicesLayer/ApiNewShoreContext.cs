using DomainLayer.Models.ApiData;
using Microsoft.EntityFrameworkCore;


namespace ServicesLayer
{
    public class ApiNewShoreContext : DbContext
    {
        public ApiNewShoreContext( DbContextOptions options ) : base(options)
        {
        }
        public DbSet<DataApiFlights> DataApiFlights { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }
    }
}
