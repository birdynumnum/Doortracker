using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configuration
{
    public class CustomerConfiguration : EntityBaseConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            Property(e => e.LastName).IsRequired().HasMaxLength(50);
            Property(f => f.MobileNumber).IsRequired().HasMaxLength(14);
            Property(d => d.RegistrationDate).HasColumnType("datetime2");
        }
    }
}
