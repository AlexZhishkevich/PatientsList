using PatientsList.Domain.Models.DateAndTime;

namespace PatientsList.Domain.Models.Filters
{
    public record DateTimeSearchFilter(
        SearchOption Option,
        DateTimeCustomModel Value);
}
