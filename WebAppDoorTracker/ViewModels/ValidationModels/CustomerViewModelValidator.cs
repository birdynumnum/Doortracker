using FluentValidation;

namespace WebAppDoorTracker.ViewModels.ValidationModels
{
    public class CustomerViewModelValidator : AbstractValidator<CustomerDTO>
    {
        public CustomerViewModelValidator()
        {
            RuleFor(customer => customer.FirstName).NotEmpty()
             .Length(1, 100).WithMessage("First Name must be between 1 - 100 characters");

            RuleFor(customer => customer.LastName).NotEmpty()
                .Length(1, 100).WithMessage("Last Name must be between 1 - 100 characters");

            RuleFor(customer => customer.EMail).NotEmpty()
                .Length(1, 100).WithMessage("Email must be between 1 - 50 characters");

            RuleFor(customer => customer.MobileNumber).NotEmpty().Matches(@"^\d{10}$")
               .Length(10).WithMessage("Mobile phone must have 10 digits");
        }
    }
}