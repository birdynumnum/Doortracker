using FluentValidation;

namespace WebAppDoorTracker.ViewModels.ValidationModels
{
    public class DoorViewModelValidator : AbstractValidator<DoorDTO>
    {
        public DoorViewModelValidator()
        {
            RuleFor(door => door.Model).NotEmpty()
              .Length(1, 100).WithMessage("Model Name must be between 1 - 100 characters");

            RuleFor(door => door.Make).NotEmpty()
             .Length(1, 100).WithMessage("Make Name must be between 1 - 100 characters");
        }
    }
}