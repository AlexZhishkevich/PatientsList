using PatientsList.Application.Abstractions.Cqrs;

namespace PatientsList.Application.Patients.Remove
{
    public sealed record RemovePatientCommand(Guid PatientGuid) : ICommand;
}
