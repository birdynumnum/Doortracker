
using System.Data.Entity;
namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext context;

        public UnitOfWork(DbContext _context)
        {
            context = _context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
