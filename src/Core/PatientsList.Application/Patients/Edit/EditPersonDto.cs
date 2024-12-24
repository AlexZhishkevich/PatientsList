using PatientsList.Application.Patients.Create;
using PatientsList.Domain.Models.People;
using System.Text.Json.Serialization;

namespace PatientsList.Application.Patients.Edit
{
    public class EditPersonDto : NewPersonDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        internal new Patient ToDomainModel()
        {
            return new Patient
            {
                Id = Id,
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
