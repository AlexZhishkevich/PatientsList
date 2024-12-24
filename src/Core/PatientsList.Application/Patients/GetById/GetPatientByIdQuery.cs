using PatientsList.Application.Abstractions.Cqrs;

namespace PatientsList.Application.Patients.GetById
{
    public sealed record GetPatientByIdQuery(Guid PatientId) : IQuery<PatientInfoDto>;
}
