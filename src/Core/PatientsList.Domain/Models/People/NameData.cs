namespace PatientsList.Domain.Models.People
{
    public class NameData
    {
        public Guid Id { get; set; }

        public NameUsageType? Use { get; set; }

        public string Family { get; set; } = string.Empty;

        public List<string>? GivenNames { get; set; } = new();

        public static NameData Default => new() { Use = NameUsageType.Anonymous };
    }
}
