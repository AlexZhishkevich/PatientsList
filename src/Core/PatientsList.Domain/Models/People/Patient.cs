namespace PatientsList.Domain.Models.People
{
    public class Patient
    {
        public Guid Id { get; set; }
        public NameData Name { get; set; } = NameData.Default;

        public GenderType? Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool? Active { get; set; }
    }
}
