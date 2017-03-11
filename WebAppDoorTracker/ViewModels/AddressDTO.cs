using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebAppDoorTracker.ViewModels.ValidationModels;

namespace WebAppDoorTracker.ViewModels
{
    public class AddressDTO : IValidatableObject
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new AddressViewModelValidator();
            var result = validator.Validate(this);

            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}