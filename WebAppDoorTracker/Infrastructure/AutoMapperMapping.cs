using AutoMapper;
using Domain;
using WebAppDoorTracker.ViewModels;

namespace WebAppDoorTracker.Infrastructure
{
    public static class AutoMapperMapping
    {
        public static void Config()
        {
            Mapper.Initialize(config =>
                {
                    config.CreateMap<Customer, CustomerDTO>();
                    config.CreateMap<Door, DoorDTO>();
                    config.CreateMap<Address, AddressDTO>();
                });
        }
    }
}