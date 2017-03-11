using DataLayer;
using DataLayer.Repository;
using Domain;
using StructureMap.Configuration.DSL;
using System.Data.Entity;
using StructureMap.Web;

namespace WebAppDoorTracker.DependencyResolution
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For(typeof(IEntityBaseRepository<Customer>)).Use(typeof(EntityBaseRepository<Customer>));
            For(typeof(IEntityBaseRepository<Door>)).Use(typeof(EntityBaseRepository<Door>));
            For(typeof(IEntityBaseRepository<Address>)).Use(typeof(EntityBaseRepository<Address>));
            For(typeof(IEntityBaseRepository<Error>)).Use(typeof(EntityBaseRepository<Error>));

            For<DbContext>().HttpContextScoped().Use<DoorContext>();

            For<IUnitOfWork>().Use<UnitOfWork>();
        }
    }
}
