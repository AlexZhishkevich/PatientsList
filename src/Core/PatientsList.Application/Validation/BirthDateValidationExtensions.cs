namespace PatientsList.Application.Validation
{
    internal static class BirthDateValidationExtensions
    {
        internal const int MaxAge = 120;

        internal static bool BeInPast(DateTime dateOfBirth)
        {
            return dateOfBirth < DateTime.Now;
        }

        internal static bool BeValidAge(DateTime dateOfBirth, int maxAge)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;

            if (DateTime.Today.AddYears(-age) < dateOfBirth)
                age--;

            return age <= maxAge;
        }
    }
}
