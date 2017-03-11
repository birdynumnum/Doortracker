using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppDoorTracker.ViewModels;

namespace WebAppDoorTracker.ExtensionMethods
{
    public static class DoorUpdates
    {
        public static void UpdateDoor(this Door door, DoorDTO doordto)
        {
            door.Id = doordto.Id;
            door.Make = doordto.Make;
            door.Model = doordto.Model;
            door.NoOfWarrantyYears = doordto.NoOfWarrantyYears;
            door.YearOfManufacture = doordto.YearOfManufacture;
            door.DateOfInstallation = doordto.DateOfInstallation;

        }
    }
}