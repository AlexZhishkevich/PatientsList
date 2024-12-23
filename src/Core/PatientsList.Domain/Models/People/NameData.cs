namespace PatientsList.Domain.Models.People
{
    public class NameData
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Identifies the purpose for this name.
        /// </summary>
        public NameUsageType? Use { get; set; }

        /// <summary>
        /// Family name (often called 'Surname')
        /// </summary>
        public string Family { get; set; } = string.Empty;

        /// <summary>
        /// Given names (not always 'first'). Includes middle names
        /// <br/>This repeating element order: Given Names appear in the correct order for presenting the name
        /// </summary>
        public List<string>? GivenNames { get; set; } = new();

        public static NameData Default => new() { Use = NameUsageType.Anonymous };
    }
}
