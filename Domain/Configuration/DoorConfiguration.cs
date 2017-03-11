using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configuration
{
    public class DoorConfiguration : EntityBaseConfiguration<Door>
    {
        public DoorConfiguration()
        {
            HasKey(e => e.Id);
            Property(f => f.Make).IsRequired().HasMaxLength(50);
            Property(g => g.Model).IsRequired().HasMaxLength(50);
            Property(h => h.NoOfWarrantyYears).IsRequired();
            Property(d => d.DateOfInstallation).HasColumnType("datetime2");
            Property(e => e.YearOfManufacture).HasColumnType("datetime2");
        }
    }
}
