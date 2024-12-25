using CSharpFunctionalExtensions;
using FluentValidation;
using PatientsList.Application.Abstractions.Cqrs;
using PatientsList.Domain.Abstractions;
using System.Text;

namespace PatientsList.Application.Patients.Create
{
    internal sealed class CreatePatientCommandHandler : ICommandHandler<CreatePatientCommand, Guid>
    {
        private readonly IPatientsRepository _repository;
        private readonly IValidator<NewPersonDto> _validator;

        public CreatePatientCommandHandler(
            IPatientsRepository repository,
            IValidator<NewPersonDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<Guid>> Handle(
            CreatePatientCommand request,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.PersonDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = new StringBuilder();

                foreach (var error in validationResult.Errors)
                    errors.AppendLine(error.ErrorMessage);

                return Result.Failure<Guid>(errors.ToString());
            }

            var personData = request.PersonDto.ToDomainModel();

            var creationResult = await _repository
                .AddAsync(personData, cancellationToken);

            return creationResult;
        }
    }
}
