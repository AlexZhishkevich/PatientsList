using CSharpFunctionalExtensions;
using PatientsList.Application.Abstractions.Cqrs;
using PatientsList.Domain.Abstractions;

namespace PatientsList.Application.Patients.Create
{
    internal sealed class CreatePatientCommandHandler : ICommandHandler<CreatePatientCommand, Guid>
    {
        private readonly IPatientsRepository _repository;

        public CreatePatientCommandHandler(IPatientsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(
            CreatePatientCommand request,
            CancellationToken cancellationToken)
        {
            var personData = request.PersonDto.ToDomainModel();

            var creationResult = await _repository
                .AddAsync(personData, cancellationToken);

            return creationResult;
        }
    }
}
