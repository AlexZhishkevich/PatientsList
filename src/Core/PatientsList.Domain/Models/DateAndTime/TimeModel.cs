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

        public bool Equals(DateTime dateTime)
        {
            if (Minutes is { } minutes)
            {
                return
                    dateTime.Hour == Hours &&
                    dateTime.Minute == minutes;
            }
            else
                return dateTime.Hour == Hours;
        }

        public bool CheckIfDateIsLess(DateTime dateTime)
        {
            if (Minutes is { } minutes)
            {
                var minutesDate = new DateTime(
                    year: dateTime.Year,
                    month: dateTime.Month,
                    day: dateTime.Day,
                    hour: Hours,
                    minute: minutes,
                    dateTime.Second);

                return dateTime < minutesDate;
            }
            else
                return dateTime.Hour < Hours;
        }

        public bool CheckIfDateIsGreater(DateTime dateTime)
        {
            if (Minutes is { } minutes)
            {
                var minutesDate = new DateTime(
                    year: dateTime.Year,
                    month: dateTime.Month,
                    day: dateTime.Day,
                    hour: Hours,
                    minute: minutes,
                    dateTime.Second);

                return dateTime > minutesDate;
            }
            else
                return dateTime.Hour > Hours;
        }
    }
}
