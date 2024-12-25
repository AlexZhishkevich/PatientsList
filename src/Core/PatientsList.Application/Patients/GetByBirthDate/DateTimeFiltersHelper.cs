using CSharpFunctionalExtensions;
using PatientsList.Domain.Models.DateAndTime;
using PatientsList.Domain.Models.Filters;
using SearchOption = PatientsList.Domain.Models.Filters.SearchOption;

namespace PatientsList.Application.Patients.GetByBirthDate
{
    internal static class DateTimeFiltersHelper
    {
        private const char DateAndTimeSeparator = 'T';
        private const char DateValuesSeparator = '-';
        private const char TimeValuesSeparator = ':';

        private const int FilterTypeTagLength = 2;
        private const int OnlyDateLength = 10;
        private const int DateTimeWithMinutesLength = 16;

        private static readonly Dictionary<string, SearchOption> _searchOptions = new()
        {
            { "eq", SearchOption.Equals},
            { "ne", SearchOption.NotEquals},
            { "lt", SearchOption.LessThan},
            { "gt", SearchOption.GreaterThan},
            { "ge", SearchOption.GreaterThanOrEqual},
            { "le", SearchOption.LessThanOrEqual},
            { "sa", SearchOption.StartsAfter},
            { "eb", SearchOption.EndsBefore},
            { "ap", SearchOption.AnyPart},
        };

        internal static Result<IList<DateTimeSearchFilter>> ConvertToFilters(
            this IList<string> filterStrings)
        {
            var resultFilters = new List<DateTimeSearchFilter>();

            if (filterStrings.Count == 0)
                return resultFilters;

            foreach (var filterString in filterStrings)
            {
                if (filterString.Length < 6)
                    return Result.Failure<IList<DateTimeSearchFilter>>($"Incorrect filter value length ({filterString})");

                var filterTypeCode = filterString[..FilterTypeTagLength];

                if (!_searchOptions.TryGetValue(filterTypeCode, out var searchOption))
                    return Result.Failure<IList<DateTimeSearchFilter>>($"Incorrect filter type ({filterTypeCode})");

                var dateTimeString = filterString[FilterTypeTagLength..];

                if (dateTimeString.Length >= DateTimeWithMinutesLength &&
                    DateTime.TryParse(dateTimeString, out var dateTime))
                {
                    dateTime = dateTime.ToUniversalTime();
                    resultFilters.Add(new DateTimeSearchFilter(
                        searchOption,
                        new DateTimeCustomModel(
                            new DateDataModel(dateTime),
                            new TimeModel(dateTime))));

                    continue;
                }

                string dateString;
                string timeString = string.Empty;

                if (!dateTimeString.Contains(DateAndTimeSeparator))
                    dateString = dateTimeString;
                else
                {
                    var separatorIndex = dateTimeString.IndexOf(DateAndTimeSeparator);
                    dateString = dateTimeString[..separatorIndex];
                    timeString = dateTimeString[(separatorIndex + 1)..];

                    if (dateString.Length != OnlyDateLength)
                        return Result.Failure<IList<DateTimeSearchFilter>>($"Incorrect date format ({dateString})");

                    if (timeString.Length > 5)
                        timeString = timeString[..5]; // TODO: implement time zones
                }

                var dateResult = dateString.ReadDate();
                var timeResult = timeString.ReadTime();

                if (dateResult.IsFailure)
                    return Result.Failure<IList<DateTimeSearchFilter>>($"Incorrect date format ({dateString}): {dateResult.Error}");

                if (timeResult.IsFailure)
                    return Result.Failure<IList<DateTimeSearchFilter>>($"Incorrect time format ({timeString}): {timeResult.Error}");

                resultFilters.Add(new DateTimeSearchFilter(
                       searchOption,
                       new DateTimeCustomModel(
                           dateResult.Value,
                           timeResult.Value)));
            }

            return Result.Success<IList<DateTimeSearchFilter>>(resultFilters);
        }

        private static Result<DateDataModel> ReadDate(this string dateString)
        {
            var dateValues = dateString.Split(DateValuesSeparator);

            int year = 0;
            int? month = null;
            int? day = null;

            if (dateValues.Length > 0)
            {
                if (!int.TryParse(dateValues[0], out year))
                    return Result.Failure<DateDataModel>($"Incorrect year value ({dateValues[0]})");

                if (dateValues.Length > 1)
                {
                    if (int.TryParse(dateValues[1], out var monthValue))
                        month = monthValue;
                    else
                        return Result.Failure<DateDataModel>($"Incorrect month value ({dateValues[1]})");

                    if (dateValues.Length > 2)
                    {
                        if(int.TryParse(dateValues[2], out var dayValue))
                            day = dayValue;
                        else
                            return Result.Failure<DateDataModel>($"Incorrect day value ({dateValues[2]})");
                    }
                }
            }

            return Result.Success(new DateDataModel(year, month, day));
        }

        private static Result<TimeModel?> ReadTime(this string timeString)
        {
            if (string.IsNullOrEmpty(timeString)) 
                return Result.Success<TimeModel?>(null);

            var timeValues = timeString.Split(TimeValuesSeparator);

            int hours = 0;
            int? minutes = null;

            if (timeValues.Length > 0)
            {
                if (!int.TryParse(timeValues[0], out hours))
                     return Result.Failure<TimeModel?>($"Incorrect hours value ({timeValues[0]})");

                if (timeValues.Length > 1)
                {
                    if (int.TryParse(timeValues[1], out var minutesValue))
                        minutes = minutesValue;
                    else
                        return Result.Failure<TimeModel?>($"Incorrect minutes value ({timeValues[1]})");
                }
            }

            return Result.Success<TimeModel?>(new TimeModel(hours, minutes));
        }
    }
}
