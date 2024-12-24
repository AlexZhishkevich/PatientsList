using CSharpFunctionalExtensions;
using PatientsList.Application.Abstractions.Cqrs;
using PatientsList.Domain.Abstractions;

namespace PatientsList.Application.Patients.GetByBirthDate
{
    internal class GetPatientsByBirthDateQueryHandler
        : IQueryHandler<GetPatientsByBirthDateQuery, List<PatientInfoDto>>
    {
        private readonly IPatientsRepository _repository;

        public GetPatientsByBirthDateQueryHandler(IPatientsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<PatientInfoDto>>> Handle(
            GetPatientsByBirthDateQuery request,
            CancellationToken cancellationToken)
        {
            var filtersResult = request.DateFilters.ConvertToFilters();

            if (filtersResult.IsFailure)
                return Result.Failure<List<PatientInfoDto>>(filtersResult.Error);

            var patientsResult = await _repository
                .GetByBirthDateAsync(filtersResult.Value, cancellationToken);

            if (patientsResult.IsFailure)
                return Result.Failure<List<PatientInfoDto>>(patientsResult.Error);

            return Result.Success(patientsResult
                .Value
                .Select(p => PatientInfoDto.CreateFromDomainModel(p))
                .ToList());
        }
    }
}
