using CSharpFunctionalExtensions;
using PatientsList.Domain.Models.Filters;
using PatientsList.Domain.Models.People;

namespace PatientsList.Domain.Abstractions
{
    public interface IPatientsRepository
    {
        /// <summary>
        /// Get patient data by identifier
        /// </summary>
        /// <param name="nameGuid"></param>
        /// <returns></returns>
        Task<Result<Patient>> GetByIdAsync(
            Guid patientId,
            CancellationToken token);

        /// <summary>
        /// Get patients by <see cref="Patient.BirthDate"/>
        /// </summary>
        /// <param name="dateSearchFilters">collection of search filters</param>
        /// <returns><see cref="Result"/> item with matching patients in case of success, otherwise, error description</returns>
        Task<Result<IList<Patient>>> GetByBirthDateAsync(
            IList<DateTimeSearchFilter> dateSearchFilters,
            CancellationToken token);

        /// <summary>
        /// Add new patient to storage
        /// </summary>
        /// <param name="patient">patient data</param>
        /// <returns><see cref="Result"/> item with created patient GUID in case of success, otherwise, error description</returns>
        Task<Result<Guid>> AddAsync(
            Patient patient,
            CancellationToken token);

        /// <summary>
        /// Edit patient data in the storage
        /// </summary>
        /// <param name="patient">patient data to be updated</param>
        /// <returns><see cref="Result"/> with operation success indication</returns>
        Task<Result> EditAsync(
            Patient patient,
            CancellationToken token);

        /// <summary>
        /// Remove patient data from the storage
        /// </summary>
        /// <param name="nameGuid">patient name identifier</param>
        /// <returns><see cref="Result"/> with operation success indication</returns>
        Task<Result> RemoveAsync(
            Guid patientId,
            CancellationToken token);
    }
}
