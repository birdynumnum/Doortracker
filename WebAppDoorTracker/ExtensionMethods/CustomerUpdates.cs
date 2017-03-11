using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppDoorTracker.ViewModels;

namespace WebAppDoorTracker.ExtensionMethods
{
    public static class CustomerUpdates
    {
        public static void UpdateCustomer(this Customer customer, CustomerDTO customerdto)
        {
            customer.Id = customerdto.Id;
            customer.FirstName = customerdto.FirstName;
            customer.LastName = customer.LastName;
            customer.EMail = customerdto.EMail;
            customer.MobileNumber = customer.MobileNumber;
            customer.RegistrationDate = customerdto.RegistrationDate;
        }
    }
}