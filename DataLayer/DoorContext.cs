using Domain;
using Domain.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataLayer
{
    public class DoorContext : DbContext
    {
        public DoorContext()
            : base("name=DoorTracker")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new DoorConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Error> Error { get; set; }
    }
}
