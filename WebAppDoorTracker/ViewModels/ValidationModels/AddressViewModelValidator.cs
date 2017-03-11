using FluentValidation;

namespace WebAppDoorTracker.ViewModels.ValidationModels
{
    public class AddressViewModelValidator : AbstractValidator<AddressDTO>
    {
        public AddressViewModelValidator()
        {
            RuleFor(address => address.StreetName).NotEmpty()
            .Length(1, 100).WithMessage("Street Name must be between 1 - 100 characters");

            RuleFor(address => address.StreetNumber).NotEmpty()
                .Length(1, 100).WithMessage("Street number required");

            RuleFor(address => address.PostalCode).NotEmpty()
                .Length(1, 100).WithMessage("Postal Code required");
        }
    }
}