using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebAppDoorTracker.ViewModels.ValidationModels;

namespace WebAppDoorTracker.ViewModels
{
    public class DoorDTO : IValidatableObject
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime YearOfManufacture { get; set; }
        public DateTime DateOfInstallation { get; set; }
        public int NoOfWarrantyYears { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new DoorViewModelValidator();
            var result = validator.Validate(this);

            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}