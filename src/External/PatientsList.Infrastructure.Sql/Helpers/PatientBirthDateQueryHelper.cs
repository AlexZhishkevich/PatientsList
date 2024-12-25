using PatientsList.Domain.Models.DateAndTime;
using PatientsList.Domain.Models.Filters;
using PatientsList.Infrastructure.Sql.Entities;
using SearchOption = PatientsList.Domain.Models.Filters.SearchOption;

namespace PatientsList.Infrastructure.Sql.Helpers
{
    internal static class PatientBirthDateQueryHelper
    {
        internal static IQueryable<PatientEntity> AddBirthDateQueryFilters(
            this IQueryable<PatientEntity> query,
            IList<DateTimeSearchFilter> dateTimeSearchFilters)
        {
            if (dateTimeSearchFilters.Count == 0)
                return query;

            foreach (var filter in dateTimeSearchFilters)
                query = query.AddFilterQuery(filter);

            return query;
        }

        private static IQueryable<PatientEntity> AddFilterQuery(
            this IQueryable<PatientEntity> query,
            DateTimeSearchFilter searchFilter)
        {
            var option = searchFilter.Option;
            var dateTimeModel = searchFilter.Value;

            var intervals = GetCorrectTimeInterval(dateTimeModel);

            var startInterval = intervals.startInterval;
            var endInterval = intervals.endInterval;

            if (option == SearchOption.Equals)
            {
                query = query.Where(p => 
                    p.BirthDate >= startInterval &&
                    p.BirthDate <= endInterval);
            }
            else if (option == SearchOption.NotEquals)
            {
                query = query.Where(p =>
                    p.BirthDate < startInterval ||
                    p.BirthDate > endInterval);
            }
            else if (option == SearchOption.LessThan)
            {
                query = query.Where(p =>
                    p.BirthDate < startInterval);
            }
            else if (option == SearchOption.GreaterThan)
            {
                query = query.Where(p =>
                    p.BirthDate > endInterval);
            }
            else if (option == SearchOption.LessThanOrEqual)
            {
                query = query.Where(p =>
                    p.BirthDate <= endInterval);
            }
            else if (option == SearchOption.GreaterThanOrEqual)
            {
                query = query.Where(p =>
                    p.BirthDate >= startInterval);
            }
            // This options are mostly suitable for time spans, not for DateTimes
            else if (option == SearchOption.StartsAfter)
            {
                query = query.Where(p =>
                    p.BirthDate > endInterval);
            }
            else if (option == SearchOption.EndsBefore)
            {
                query = query.Where(p =>
                    p.BirthDate < startInterval);
            }
            else if (option == SearchOption.AnyPart)
            {
                query = query.Where(p =>
                    p.BirthDate >= startInterval &&
                    p.BirthDate <= endInterval);
            }

            return query;
        }

        private static (DateTime startInterval, DateTime endInterval) GetCorrectTimeInterval(
            DateTimeCustomModel dateTimeModel)
        {
            DateTime startInterval;
            DateTime endInterval;

            var date = dateTimeModel.Date;
            var time = dateTimeModel.Time;

            var year = date.Year;

            if (date.Month is not { } notNullMonth)
            {
                // set year interval
                startInterval = new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                endInterval = startInterval
                    .AddYears(1)
                    .AddMilliseconds(-1);
                return (startInterval, endInterval);
            }

            if (date.Day is not { } notNullDay)
            {
                // set month interval
                startInterval = new DateTime(year, notNullMonth, 1, 0, 0, 0, DateTimeKind.Utc);
                endInterval = startInterval
                    .AddMonths(1)
                    .AddMilliseconds(-1);
                return (startInterval, endInterval);
            }

            if (time is not { } notNullTime)
            {
                // set day interval
                startInterval = new DateTime(year, notNullMonth, notNullDay, 0, 0, 0, DateTimeKind.Utc);
                endInterval = startInterval
                    .AddDays(1)
                    .AddMilliseconds(-1);
                return (startInterval, endInterval);
            }

            if (notNullTime.Minutes is not { } notNulMinutes)
            {
                // set hour interval
                startInterval = new DateTime(year, notNullMonth, notNullDay, notNullTime.Hours, 0, 0, DateTimeKind.Utc);
                endInterval = startInterval
                    .AddHours(1)
                    .AddMilliseconds(-1);
                return (startInterval, endInterval);
            }

            // set minute interval
            startInterval = new DateTime(year, notNullMonth, notNullDay, notNullTime.Hours, notNulMinutes, 0, DateTimeKind.Utc);
            endInterval = startInterval
                .AddMinutes(1)
                .AddMilliseconds(-1);
            return (startInterval, endInterval);
        }
    }
}
