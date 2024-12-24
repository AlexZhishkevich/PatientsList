using CSharpFunctionalExtensions;
using PatientsList.Application.Abstractions.Cqrs;
using PatientsList.Domain.Abstractions;

namespace PatientsList.Application.Patients.GetById
{
    internal class GetPatientByIdQueryHandler : IQueryHandler<GetPatientByIdQuery, PatientInfoDto>
    {
        private readonly IPatientsRepository _repository;

        public GetPatientByIdQueryHandler(IPatientsRepository patientsRepository)
        {
            _repository = patientsRepository;
        }

        public async Task<Result<PatientInfoDto>> Handle(
            GetPatientByIdQuery request,
            CancellationToken cancellationToken)
        {
            var dataResult = await _repository.GetByIdAsync(
                request.PatientId,
                cancellationToken);

            if (dataResult.IsFailure)
                return Result.Failure<PatientInfoDto>(dataResult.Error);

            return Result.Success(
                PatientInfoDto.CreateFromDomainModel(dataResult.Value));
        }
    }
}
