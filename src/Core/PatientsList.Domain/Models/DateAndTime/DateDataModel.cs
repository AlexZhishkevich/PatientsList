namespace PatientsList.Domain.Models.DateAndTime
{
    public class DateDataModel
    {
        public int Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }

        public DateDataModel(DateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
        }

        public DateDataModel(
            int year,
            int? month,
            int? day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public bool Equals(DateTime dateTime)
        {
            if (Month is { } month)
            {
                if (Day is { } day)
                {
                    return
                        dateTime.Year == Year &&
                        dateTime.Month == month &&
                        dateTime.Day == day;
                }

                return
                    dateTime.Year == Year &&
                    dateTime.Month == month;
            }
            else
                return dateTime.Year == Year;
        }

        public bool CheckIfDateIsLess(DateTime dateTime)
        {
            if (Month is { } month)
            {
                if (Day is { } day)
                {
                    var fullDate = new DateTime(year: Year, month: month, day: day);
                    return dateTime < fullDate;
                }

                var monthDate = new DateTime(
                    year: Year,
                    month: month,
                    day: 1);

                return dateTime < monthDate;
            }
            else
                return dateTime.Year < Year;
        }

        public bool CheckIfDateIsGreater(DateTime dateTime)
        {
            if (Month is { } month)
            {
                if (Day is { } day)
                {
                    var fullDate = new DateTime(year: Year, month: month, day: day);
                    return dateTime > fullDate;
                }

                var monthDate = new DateTime(
                    year: Year,
                    month: month,
                    day: DateTime.DaysInMonth(Year, month));

                return dateTime > monthDate;
            }
            else
                return dateTime.Year > Year;
        }
    }
}
