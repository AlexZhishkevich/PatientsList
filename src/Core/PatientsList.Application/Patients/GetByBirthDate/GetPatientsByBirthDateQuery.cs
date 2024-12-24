using PatientsList.Application.Abstractions.Cqrs;

namespace PatientsList.Application.Patients.GetByBirthDate
{
    public sealed record GetPatientsByBirthDateQuery(
        IList<string> DateFilters) : IQuery<List<PatientInfoDto>>;
}
