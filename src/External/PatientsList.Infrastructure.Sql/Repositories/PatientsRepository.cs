﻿using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientsList.Domain.Abstractions;
using PatientsList.Domain.Models.Filters;
using PatientsList.Domain.Models.People;
using PatientsList.Infrastructure.Sql.Entities;
using PatientsList.Infrastructure.Sql.Helpers;

namespace PatientsList.Infrastructure.Sql.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly PatientsDbContext _context;
        private readonly ILogger _logger;

        public PatientsRepository(
            PatientsDbContext context,
            ILogger<PatientsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<Result<Patient>> GetByIdAsync(
            Guid patientId,
            CancellationToken token)
        {
            try
            {
                var patient = await _context
                    .PatientsInfoSet!
                    .AsNoTracking()
                    .Include(p => p.NameDataEntity)
                    .FirstOrDefaultAsync(p => p.Id.Equals(patientId), cancellationToken: token);

                if (patient == null)
                {
                    var message = $"Patient with specified id not found ({patientId})";
                    _logger.LogWarning(message);
                    return Result.Failure<Patient>(message);
                }

                return Result.Success(patient.ToDomainModel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Patient data receiving error ({patientId})");
                return Result.Failure<Patient>(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<Result<IList<Patient>>> GetByBirthDateAsync(
            IList<DateTimeSearchFilter> dateSearchFilters,
            CancellationToken token)
        {
            if (dateSearchFilters.Count < 1)
            {
                _logger.LogWarning("Attempt to get patients by 'BirthDate' with empty filters");
                return Result.Failure<IList<Patient>>("Filters collection is empty");
            }

            try
            {
                var query = _context
                   .PatientsInfoSet!
                   .Include(p => p.NameDataEntity)
                   .AsNoTracking();
                  
                query = query.AddBirthDateQueryFilters(dateSearchFilters);

                var patientEntities = await query
                    .ToListAsync(cancellationToken: token);

                IList<Patient> patients = patientEntities
                    .Select(p => p.ToDomainModel())
                    .ToList();

                return Result.Success(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Patient filtered data receiving error");
                return Result.Failure<IList<Patient>>(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<Result<Guid>> AddAsync(
            Patient patient,
            CancellationToken token)
        {
            try
            {
                var patientNameDataEntity = PatientNameDataEntity
                    .CreateFromDomainModel(patient.Name);

                await _context
                    .PatientNamesSet!
                    .AddAsync(patientNameDataEntity, token);

                token.ThrowIfCancellationRequested();

                var patientInfoEntity = new PatientEntity
                {
                    NameDataEntity = patientNameDataEntity,
                    Gender = patient.Gender != null
                        ? (byte)patient.Gender
                        : null,
                    BirthDate = patient.BirthDate.ToUniversalTime(),
                    Active = patient.Active,
                };

                await _context
                    .PatientsInfoSet!
                    .AddAsync(patientInfoEntity, token);

                await _context.SaveChangesAsync(token);

                return Result.Success(patientInfoEntity.Id);
            }
            catch (OperationCanceledException cancelledEx)
            {
                var message = $"Patient data adding has been cancelled";
                _logger.LogWarning(cancelledEx, message);
                return Result.Failure<Guid>(message);
            }
            catch (Exception ex)
            {
                return Result.Failure<Guid>(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<Result> EditAsync(
            Patient patient,
            CancellationToken token)
        {
            try
            {
                var currentPatientData = await _context
                    .PatientsInfoSet!
                    .Include(p => p.NameDataEntity)
                    .FirstOrDefaultAsync(p => p.Id.Equals(patient.Id), cancellationToken: token);

                if (currentPatientData == null)
                {
                    var message = $"Patient with specified id not found ({patient.Id})";
                    _logger.LogWarning(message);
                    return Result.Failure<Patient>(message);
                }

                currentPatientData.Gender = patient.Gender != null
                        ? (byte)patient.Gender
                        : null;
                currentPatientData.BirthDate = patient.BirthDate.ToUniversalTime();
                currentPatientData.Active = patient.Active;

                var nameInfo = currentPatientData.NameDataEntity;
                var updatedNameInfo = PatientNameDataEntity
                    .CreateFromDomainModel(patient.Name);

                nameInfo ??= new PatientNameDataEntity();
                nameInfo.Use = updatedNameInfo.Use;
                nameInfo.Family = updatedNameInfo.Family;
                nameInfo.GivenNames = updatedNameInfo.GivenNames;

                token.ThrowIfCancellationRequested();

                await _context.SaveChangesAsync(token);

                return Result.Success();
            }
            catch (OperationCanceledException cancelledEx)
            {
                var message = $"Patient data update has been cancelled";
                _logger.LogWarning(cancelledEx, message);
                return Result.Failure<Guid>(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Patient data updating error ({patient.Id})");
                return Result.Failure<Patient>(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<Result> RemoveAsync(
            Guid patientId,
            CancellationToken token)
        {
            try
            {
                var patient = await _context
                    .PatientsInfoSet!
                    .Include(p => p.NameDataEntity)
                    .FirstOrDefaultAsync(t => t.Id == patientId, cancellationToken: token);

                if (patient == null)
                {
                    var message = $"Patient with specified id not found ({patientId})";
                    _logger.LogWarning(message);
                    return Result.Failure<Patient>(message);
                }

                token.ThrowIfCancellationRequested();

                _context.PatientNamesSet!.Remove(patient.NameDataEntity!);

                _context.PatientsInfoSet!.Remove(patient);

                await _context.SaveChangesAsync(token);

                return Result.Success();
            }
            catch(OperationCanceledException cancelledEx)
            {
                var message = $"Patient data removing has been cancelled ({patientId})";
                _logger.LogWarning(cancelledEx, message);
                return Result.Failure(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Patient data removing error ({patientId})");
                return Result.Failure(ex.Message);
            }
        }
    }
}
