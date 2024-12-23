namespace PatientsList.Domain.Models.People
{
    /// <summary>
    /// The gender of a person used for administrative purposes.
    /// <br/>(<see cref="https://build.fhir.org/valueset-administrative-gender.html"/>
    /// </summary>
    public enum GenderType
    {
        Male,
        Female,
        Other,
        Unknown
    }
}
