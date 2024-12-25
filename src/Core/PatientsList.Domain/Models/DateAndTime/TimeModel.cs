namespace PatientsList.Domain.Models.DateAndTime
{
    public class TimeModel
    {
        public int Hours { get; set; }
        public int? Minutes { get; set; }

        public TimeModel(DateTime dateTime)
        {
            Hours = dateTime.Hour;
            Minutes = dateTime.Minute;
        }

        public TimeModel(
            int hours,
            int? minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }
    }
}
