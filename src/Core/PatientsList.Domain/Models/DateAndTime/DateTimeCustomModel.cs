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

        public bool Equals(DateTime dateTime)
        {
            if (Time == null)
                return Date.Equals(dateTime);
            else
                return Date.Equals(dateTime) && 
                    Time.Equals(dateTime);
        }

        public bool CheckIfDateIsLess(DateTime dateTime)
        {
            if (Time == null)
                return Date.CheckIfDateIsLess(dateTime);

            if (Date.CheckIfDateIsLess(dateTime))
                return true;
            else if (Date.Equals(dateTime))
                return Time.CheckIfDateIsLess(dateTime);
            else 
                return false;
        }

        public bool CheckIfDateIsGreater(DateTime dateTime)
        {
            if (Time == null)
                return Date.CheckIfDateIsGreater(dateTime);

            if (Date.CheckIfDateIsGreater(dateTime))
                return true;
            else if (Date.Equals(dateTime))
                return Time.CheckIfDateIsGreater(dateTime);
            else
                return false;
        }
    }
}
