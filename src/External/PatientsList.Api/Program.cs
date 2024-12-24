using PatientsList.Api.Extensions;
using PatientsList.Application.Patients.GetByBirthDate;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(c => 
    c.RegisterServicesFromAssemblyContaining<GetPatientsByBirthDateQuery>());

builder.RegisterPostgreSQLStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.InitializePatientsDb();
app.UseAuthorization();
app.MapControllers();

app.Run();
