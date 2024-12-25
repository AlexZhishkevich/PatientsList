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
    }
}
