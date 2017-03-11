using Domain;
using WebAppDoorTracker.ViewModels;

namespace WebAppDoorTracker.ExtensionMethods
{
    public static class AddressUpdater
    {
        public static void UpdateAddress(this Address address, AddressDTO addressdto)
        {
            address.Id = addressdto.Id;
            address.StreetName = addressdto.StreetName;
            address.StreetNumber = addressdto.StreetNumber;
            address.City = addressdto.City;
            address.PostalCode = addressdto.PostalCode;
        }
    }
}