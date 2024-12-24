using Microsoft.EntityFrameworkCore;
using PatientsList.Domain.Abstractions;
using PatientsList.Infrastructure.Sql;
using PatientsList.Infrastructure.Sql.Repositories;

namespace PatientsList.Api.Extensions
{
    internal static class StorageConfigurationExtensions
    {
        internal static WebApplicationBuilder RegisterPostgreSQLStorage(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<PatientsDbContext>(
               options =>
               {
                   options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(PatientsDbContext)) ?? string.Empty);
               });

            builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();

            return builder;
        }

        internal static WebApplication InitializePatientsDb(this WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PatientsDbContext>();
                context.Database.Migrate();
            }

            return app;
        }
    }
}
