using PatientsList.Domain.Models.People;

namespace PatientsList.Infrastructure.Sql.Entities
{
    public class PatientInfoEntity
    {
        public Guid Id { get; set; }

        public byte? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool? Active { get; set; }

        public Guid NameDataFK { get; set; }
        public PatientNameDataEntity NameDataEntity { get; set; } = new();

        internal Patient ToDomainModel()
        {
            return new Patient
            {
                Name = NameDataEntity.ToDomainModel(),
                Gender = Gender is { } notNullByte
                    ? (GenderType)notNullByte
                    : null,
                BirthDate = BirthDate,
                Active = Active,
            };
        }
    }
}
