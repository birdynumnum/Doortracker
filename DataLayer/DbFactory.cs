
namespace DataLayer
{
    public class DbFactory : IDbFactory
    {
        //TODO: implement IDisposable
        private DoorContext context;

        public DoorContext Init()
        {
            return context ?? (context = new DoorContext());
        }
    }
}
