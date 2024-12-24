using CSharpFunctionalExtensions;
using PatientsList.Application.Abstractions.Cqrs;
using PatientsList.Domain.Abstractions;

namespace PatientsList.Application.Patients.Remove
{
    internal sealed class RemovePatientCommandHandler : ICommandHandler<RemovePatientCommand>
    {
        private readonly IPatientsRepository _patientsRepository;

        public RemovePatientCommandHandler(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }

        public async Task<Result> Handle(
            RemovePatientCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _patientsRepository
                .RemoveAsync(request.PatientGuid, cancellationToken);

            return result;
        }
    }
}
