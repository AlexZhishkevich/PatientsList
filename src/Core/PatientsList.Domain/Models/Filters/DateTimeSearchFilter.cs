namespace PatientsList.Domain.Models.Filters
{
    public class DateTimeSearchFilter
    {
        public SearchOption Option { get; set; }

        public DateTime Value { get; set; }
    }
}
