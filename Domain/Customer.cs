using System;
using System.Collections.Generic;

namespace Domain
{
    public class Customer : IEntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public virtual List<Address> Address { get; set; }

        public Customer()
        {
            Address = new List<Address>();
        }

        public Customer(int id, string FN, string LN, string mail, string mobile, DateTime RD)
        {
            Id = id;
            FirstName = FN;
            LastName = LN;
            EMail = mail;
            MobileNumber = mobile;
            RegistrationDate = RD;
        }
    }
}
