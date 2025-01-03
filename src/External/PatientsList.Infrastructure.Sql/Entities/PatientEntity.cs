﻿using PatientsList.Domain.Models.People;

namespace PatientsList.Infrastructure.Sql.Entities
{
    public class PatientEntity
    {
        public Guid Id { get; set; }

        public byte? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool? Active { get; set; }

        public Guid NameDataFK { get; set; }
        public PatientNameDataEntity? NameDataEntity { get; set; }

        internal Patient ToDomainModel()
        {
            return new Patient
            {
                Id = Id,
                Name = NameDataEntity!.ToDomainModel(),
                Gender = Gender is { } notNullByte
                    ? (GenderType)notNullByte
                    : null,
                BirthDate = BirthDate,
                Active = Active,
            };
        }
    }
}
