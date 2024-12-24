using PatientsList.Application.Abstractions.Cqrs;

namespace PatientsList.Application.Patients.Edit
{
    public sealed record EditPatientCommand(EditPersonDto PatientData) : ICommand;
}
