
namespace Domain.Configuration
{
    public class AddressConfiguration : EntityBaseConfiguration<Address>
    {
        public AddressConfiguration()
        {
            HasKey(e => e.Id);
            Property(f => f.StreetName).IsRequired().HasMaxLength(50);
            Property(g => g.StreetNumber).IsRequired().HasMaxLength(8);
            Property(h => h.PostalCode).IsRequired();
            Property(d => d.City).IsRequired().HasMaxLength(35);
        }
    }
}
