using PatientsList.Domain.Models.People;
using System.Text.Json.Serialization;

namespace PatientsList.Application.Patients.Create
{
    public class NewPersonDto
    {
        [JsonPropertyName("nameUsage")]
        public NameUsageType? NameUsage { get; set; }

        [JsonPropertyName("family")]
        public string Family { get; set; } = string.Empty;

        [JsonPropertyName("givenNames")]
        public List<string>? GivenNames { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("gender")]
        public GenderType? Gender { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        internal Patient ToDomainModel()
        {
            return new Patient
            {
                Name = new NameData
                {
                    Use = NameUsage,
                    Family = Family,
                    GivenNames = GivenNames,
                },
                BirthDate = BirthDate,
                Gender = Gender,
                Active = Active
            };
        }
    }
}
