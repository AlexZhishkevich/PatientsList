namespace PatientsList.Domain.Models.Filters
{
    /// <summary>
    /// According to <see cref="https://www.hl7.org/fhir/search.html#date"/>
    /// </summary>
    public enum SearchOption
    {
        Equals,
        NotEquals,
        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
        StartsAfter,
        EndsBefore,
        AnyPart
    }
}
