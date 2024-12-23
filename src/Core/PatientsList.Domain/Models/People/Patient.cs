namespace PatientsList.Domain.Models.People
{
    /// <summary>
    /// Demographics and other administrative information about an individual that is the subject of potential, past, current, or future health-related care, services, or processes.
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// An identifier for this patient.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// A name associated with the individual.
        /// </summary>
        public NameData Name { get; set; } = NameData.Default;

        /// <summary>
        /// The gender that the patient is considered to have for administration and record keeping purposes.
        /// </summary>
        public GenderType? Gender { get; set; }

        /// <summary>
        /// The date of birth for the individual.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Property, that determines, whether this patient record is in active use. 
        /// <br/>Many systems use this property to mark as non-current patients, such as those that have not been seen for a period of time based on an organization's business rules.
        /// </summary>
        public bool? Active { get; set; }
    }
}
