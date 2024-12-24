using PatientsList.Application.Abstractions.Cqrs;

namespace PatientsList.Application.Patients.Create
{
    public sealed record CreatePatientCommand(NewPersonDto PersonDto) : ICommand<Guid>;
}
