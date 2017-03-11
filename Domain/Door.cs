using System;

namespace Domain
{
    public class Door : IEntityBase
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime YearOfManufacture { get; set; }
        public DateTime DateOfInstallation { get; set; }
        public int NoOfWarrantyYears { get; set; }
        public Address address { get; set; }

        public Door()
        {

        }

        public Door(int id, string make, string model, DateTime YOM, DateTime DOI, int NOWY)
        {
            Id = id;
            Make = make;
            Model = model;
            YearOfManufacture = YOM;
            DateOfInstallation = DOI;
            NoOfWarrantyYears = NOWY;
        }
    }
}
