using CSharpFunctionalExtensions;
using FluentValidation;
using PatientsList.Application.Abstractions.Cqrs;
using PatientsList.Application.Patients.Create;
using PatientsList.Domain.Abstractions;
using System.Text;

namespace PatientsList.Application.Patients.Edit
{
    internal class EditPatientCommandHandler : ICommandHandler<EditPatientCommand>
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly IValidator<EditPersonDto> _validator;

        public EditPatientCommandHandler(
            IPatientsRepository patientsRepository,
            IValidator<NewPersonDto> validator)
        {
            _patientsRepository = patientsRepository;
            _validator = validator;
        }

        public async Task<Result> Handle(
            EditPatientCommand request,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(
                request.PatientData,
                cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = new StringBuilder();

                foreach (var error in validationResult.Errors)
                    errors.AppendLine(error.ErrorMessage);

                return Result.Failure<Guid>(errors.ToString());
            }

            var patientData = request.PatientData.ToDomainModel();

            var result = await _patientsRepository
                .EditAsync(patientData, cancellationToken);

            return result;
        }
    }
}
