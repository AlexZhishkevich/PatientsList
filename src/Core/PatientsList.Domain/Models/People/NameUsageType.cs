namespace PatientsList.Domain.Models.People
{
    /// <summary>
    /// According to <see cref="https://www.hl7.org/fhir/valueset-name-use.html"/>
    /// </summary>
    public enum NameUsageType
    {
        Usual,
        Official,
        Temp,
        Nickname,
        Anonymous,
        Old,
        Maiden
    }
}
