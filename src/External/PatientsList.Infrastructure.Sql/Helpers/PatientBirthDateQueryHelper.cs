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

            if (option == SearchOption.Equals)
            {
                query = query
                    .Where(p => dateTimeModel.Equals(p.BirthDate));
            }
            else if (option == SearchOption.NotEquals)
            {
                query = query
                    .Where(p => !dateTimeModel.Equals(p.BirthDate));
            }
            else if (option == SearchOption.LessThan)
            {
                query = query
                    .Where(p => dateTimeModel.CheckIfDateIsLess(p.BirthDate));
            }
            else if (option == SearchOption.GreaterThan)
            {
                query = query
                    .Where(p => dateTimeModel.CheckIfDateIsGreater(p.BirthDate));
            }
            else if (option == SearchOption.LessThanOrEqual)
            {
                query = query
                    .Where(p => dateTimeModel.CheckIfDateIsLess(p.BirthDate) ||
                                dateTimeModel.Equals(p.BirthDate));
            }
            else if (option == SearchOption.GreaterThanOrEqual)
            {
                query = query
                    .Where(p => dateTimeModel.CheckIfDateIsGreater(p.BirthDate) ||
                                dateTimeModel.Equals(p.BirthDate));
            }
            // This options are mostly suitable for time spans, not for DateTimes
            else if (option == SearchOption.StartsAfter)
            {
                query = query
                    .Where(p => dateTimeModel.CheckIfDateIsGreater(p.BirthDate));
            }
            else if (option == SearchOption.EndsBefore)
            {
                query = query
                    .Where(p => dateTimeModel.CheckIfDateIsLess(p.BirthDate));
            }
            else if (option == SearchOption.AnyPart)
            {
                query = query
                    .Where(p => dateTimeModel.Equals(p.BirthDate));
            }

            return query;
        }
    }
}
