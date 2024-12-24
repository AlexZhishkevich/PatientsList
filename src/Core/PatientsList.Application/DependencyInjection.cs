using Microsoft.Extensions.DependencyInjection;
using PatientsList.Application.Abstractions.Cqrs;
using PatientsList.Application.Patients;
using PatientsList.Application.Patients.Create;
using PatientsList.Application.Patients.Edit;
using PatientsList.Application.Patients.GetByBirthDate;
using PatientsList.Application.Patients.GetById;
using PatientsList.Application.Patients.Remove;

namespace PatientsList.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterCqrs(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICommandHandler<CreatePatientCommand, Guid>, CreatePatientCommandHandler>();
            serviceCollection.AddScoped<ICommandHandler<EditPatientCommand>, EditPatientCommandHandler>();
            serviceCollection.AddScoped<ICommandHandler<RemovePatientCommand>, RemovePatientCommandHandler>();

            serviceCollection.AddScoped<IQueryHandler<GetPatientsByBirthDateQuery, List<PatientInfoDto>>, GetPatientsByBirthDateQueryHandler>();
            serviceCollection.AddScoped<IQueryHandler<GetPatientByIdQuery, PatientInfoDto>, GetPatientByIdQueryHandler>();

            return serviceCollection;
        }
    }
}
