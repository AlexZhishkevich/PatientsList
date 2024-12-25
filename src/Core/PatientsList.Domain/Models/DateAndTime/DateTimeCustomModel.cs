namespace PatientsList.Domain.Models.DateAndTime
{
    public class DateTimeCustomModel
    {
        public DateDataModel Date { get; }
        public TimeModel? Time { get; }

        public DateTimeCustomModel(
            DateDataModel date,
            TimeModel? time)
        {
            Date = date;
            Time = time;
        }
    }
}
