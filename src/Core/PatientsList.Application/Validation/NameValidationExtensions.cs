namespace PatientsList.Application.Validation
{
    internal static class NameValidationExtensions
    {
        internal const int MinNameLength = 1;
        internal const int MaxNameLength = 70;
        internal const int MaxGivenNamesLength = 255;

        internal static bool BeCorrectNames(List<string>? givenNames)
        {
            if (givenNames is null)
                return true;

            return givenNames.All(n => n.Length >= MinNameLength && n.Length < MaxNameLength);
        }

        internal static bool NotExceedTotalLength(List<string>? givenNames, int maxLength)
        {
            if (givenNames is null)
                return true;

            return givenNames.Sum(n => n.Length) <= maxLength;
        }
    }
}
