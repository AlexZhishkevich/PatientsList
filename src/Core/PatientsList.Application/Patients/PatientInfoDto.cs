using PatientsList.Domain.Models.People;

namespace PatientsList.Application.Patients
{
    public record PatientInfoDto(
        Guid Id,
        NameUsageType? NameUsage,
        string Family,
        List<string>? GivenNames,
        DateTime BirthDate,
        GenderType? Gender,
        bool? Active)
    {
        internal static PatientInfoDto CreateFromDomainModel(Patient patient)
        {
            var nameData = patient.Name;

            return new PatientInfoDto(
                patient.Id,
                nameData.Use,
                nameData.Family,
                nameData.GivenNames,
                patient.BirthDate,
                patient.Gender,
                patient.Active);
        }
    }
}
