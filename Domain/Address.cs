
using System.Collections.Generic;
namespace Domain
{
    public class Address : IEntityBase
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public virtual List<Door> Doors { get; set; }
        public virtual Customer Customer { get; set; }

        public Address()
        {
            Doors = new List<Door>();
        }

        public Address(string sn, string sno, string ct, string pc, List<Door> door = null)
        {
            StreetName = sn;
            StreetNumber = sno;
            City = ct;
            PostalCode = pc;
            Doors = door;
        }
    }
}
