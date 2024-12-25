using FluentValidation;
using PatientsList.Application.Validation;

namespace PatientsList.Application.Patients.Edit
{
    public sealed class EditPersonDtoValidator : AbstractValidator<EditPersonDto>
    {
        public EditPersonDtoValidator()
        {
            //TODO: add REGEX for name symbols

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Identifier can not have default value");

            var maxAge = BirthDateValidationExtensions.MaxAge;
            RuleFor(p => p.BirthDate)
                .NotEmpty().WithMessage("Date of birth is required")
                .Must(BirthDateValidationExtensions.BeInPast)
                    .WithMessage("Date of birth can not be in future")
                .Must(d => BirthDateValidationExtensions.BeValidAge(d, maxAge))
                    .WithMessage($"Patient can not be more than {maxAge} years old");

            RuleFor(p => p.Family)
                .MinimumLength(NameValidationExtensions.MinNameLength)
                    .WithMessage("'Family' can not be empty")
                .MaximumLength(NameValidationExtensions.MaxNameLength)
                    .WithMessage("'Family' value is too long");

            var maxGivenNamesLength = NameValidationExtensions.MaxGivenNamesLength;
            RuleFor(p => p.GivenNames)
                .Must(NameValidationExtensions.BeCorrectNames)
                    .WithMessage("One or more given names have incorrect length")
                .Must(d => NameValidationExtensions.NotExceedTotalLength(d, maxGivenNamesLength))
                    .WithMessage($"Total length of given names can not exceed {maxGivenNamesLength} symbols");
        }
    }
}
