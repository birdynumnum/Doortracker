using DataLayer.Repository;
using Domain;
using System.Linq;

namespace DataLayer.ExtensionMethods
{
    public static class CustomerExtension
    {
        public static bool CustomerAllReadyExists(IEntityBaseRepository<Customer> customerrepo, string email)
        {
            bool AllreadyExists = false;
            return AllreadyExists = customerrepo.GetAll().Any(c => c.EMail.ToLower() == email.ToLower());
        }
    }
}
