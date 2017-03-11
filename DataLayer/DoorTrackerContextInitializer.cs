using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataLayer
{
    public class DoorTrackerContextInitializer : DropCreateDatabaseIfModelChanges<DoorContext>
    {
        protected override void Seed(DoorContext context)
        {
            List<Customer> customers = new List<Customer>{
                
                new Customer{ Id = 1, EMail="foo@blaat.com", FirstName= "Blaat", LastName="Blaat", MobileNumber="0612345646", RegistrationDate = DateTime.Now},
                new Customer{ Id = 2, EMail="okthen@blaat.com", FirstName= "Ok", LastName="Then", MobileNumber="0665432165", RegistrationDate = DateTime.Now},
                new Customer{ Id = 3, EMail="customer1@company1.com", FirstName= "Customer1", LastName="Customer1", MobileNumber="0612345669", RegistrationDate = DateTime.Now},
                new Customer{ Id = 4, EMail="baar@baar.com", FirstName= "Foo", LastName="Bar", MobileNumber="0665436219", RegistrationDate = DateTime.Now}
        };

            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }

            context.SaveChanges();

            List<Door> doors = new List<Door>{

               new Door{Id = 1, Make="Hormann", Model="Snellop", NoOfWarrantyYears= 3, DateOfInstallation = DateTime.Now.AddDays(-100), YearOfManufacture= DateTime.Now.AddDays(-110)},
               new Door{Id = 2, Make="Solcon", Model="A321", NoOfWarrantyYears= 2, DateOfInstallation = DateTime.Now.AddDays(-10), YearOfManufacture= DateTime.Now.AddDays(-65)},
               new Door{Id = 3, Make="Solcon", Model="123A", NoOfWarrantyYears= 3, DateOfInstallation = DateTime.Now.AddDays(-100), YearOfManufacture= DateTime.Now.AddDays(-77)},
               new Door{Id = 4, Make="Solcon", Model="A321", NoOfWarrantyYears= 2, DateOfInstallation = DateTime.Now.AddDays(-10), YearOfManufacture= DateTime.Now.AddDays(-66)},
               new Door{Id = 4, Make="Hormann", Model="Xs4A31", NoOfWarrantyYears= 2, DateOfInstallation = DateTime.Now.AddDays(-10), YearOfManufacture= DateTime.Now.AddDays(-80)},
               new Door{Id = 4, Make="Berry", Model="Vista", NoOfWarrantyYears= 2, DateOfInstallation = DateTime.Now.AddDays(-10), YearOfManufacture= DateTime.Now.AddDays(-100)}
            };

            foreach (Door door in doors)
            {
                context.Doors.Add(door);
            }

            context.SaveChanges();

            Customer customerAddress = new Customer(5, "customaddress@baar.com", "Custom", "Address", "0665436219", DateTime.Now);

            var addedDoor = context.Doors.First();
            Address addres1 = new Address { };
            addres1.StreetName = "Robert Stolz Hof";
            addres1.StreetNumber = "10";
            addres1.City = "Hoorn";
            addres1.PostalCode = "1633XA";
            addres1.Customer = customerAddress;

            addres1.Doors.Add(addedDoor);


            context.Address.Add(addres1);
            context.SaveChanges();

            base.Seed(context);

        }


    }

}
