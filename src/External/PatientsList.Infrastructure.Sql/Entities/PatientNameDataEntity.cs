using PatientsList.Domain.Models.People;
using System.Text;

namespace PatientsList.Infrastructure.Sql.Entities
{
    public class PatientNameDataEntity
    {
        private const char GivenNameSeparator = ';';

        public Guid Id { get; set; }

        public byte? Use { get; set; }

        public string Family { get; set; } = string.Empty;

        /// <summary>
        /// Given name are stored as single string with separator (<see cref="GivenNameSeparator"/>)
        /// </summary>
        /// N.B.! Also can be stored as list of 'GivenNameEntity' with FK to 'NameDataEntity'
        public string? GivenNames { get; set; } = string.Empty;

        public PatientInfoEntity PatientInfoEntity { get; set; } = new();

        internal static PatientNameDataEntity CreateFromDomainModel(NameData nameData)
        {
            StringBuilder? givenNames = null;

            if (nameData.GivenNames is { Count: > 0 })
            {
                givenNames = new StringBuilder();

                foreach (var name in nameData.GivenNames)
                {
                    givenNames.Append(name);
                    givenNames.Append(GivenNameSeparator);
                }
            }

            return new PatientNameDataEntity
            {
                Id = nameData.Id,
                Family = nameData.Family,
                Use = nameData.Use is { } notNullType 
                    ? (byte)notNullType 
                    : null,
                GivenNames = givenNames?.ToString()
            };
        }

        internal NameData ToDomainModel()
        {
            return new NameData
            {
                Id = Id,
                Family = Family,
                Use = Use is { } notNullByte
                    ? (NameUsageType)notNullByte 
                    : null,
                GivenNames = GivenNames?
                    .Split(GivenNameSeparator)
                    .ToList()
            };
        }
    }
}
