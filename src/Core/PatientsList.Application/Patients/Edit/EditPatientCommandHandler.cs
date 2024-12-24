using CSharpFunctionalExtensions;
using PatientsList.Application.Abstractions.Cqrs;
using PatientsList.Domain.Abstractions;

namespace PatientsList.Application.Patients.Edit
{
    internal class EditPatientCommandHandler : ICommandHandler<EditPatientCommand>
    {
        private readonly IPatientsRepository _patientsRepository;

        public EditPatientCommandHandler(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }

        public async Task<Result> Handle(
            EditPatientCommand request,
            CancellationToken cancellationToken)
        {
            var patientData = request.PatientData.ToDomainModel();

            var result = await _patientsRepository
                .EditAsync(patientData, cancellationToken);

            return result;
        }
    }
}
