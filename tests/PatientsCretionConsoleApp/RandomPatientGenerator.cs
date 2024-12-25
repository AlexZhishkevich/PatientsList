using PatientsList.Application.Patients.Create;
using PatientsList.Domain.Models.People;

namespace PatientsCretionConsoleApp
{
    internal class RandomPatientGenerator
    {
        private static readonly Random _random = new();

        // Lists of sample names (first and last names)
        private static readonly List<string> _firstNames =
        [
            "John", "Jane", "Michael", "Emily", "David", "Sarah", "Chris", "Jessica", "James", "Sophia"
        ];

        private static readonly List<string> _lastNames =
        [
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Martinez", "Garcia", "Rodriguez"
        ];

        // Gender choices
        private static readonly List<GenderType> _genders =
        [
            GenderType.Male, GenderType.Female, GenderType.Other, GenderType.Unknown
        ];

        public static NewPersonDto GenerateRandomPatient()
        {
            var firstName = _firstNames[_random.Next(_firstNames.Count)];
            var lastName = _lastNames[_random.Next(_lastNames.Count)];

            // Create a random patient object
            var patient = new NewPersonDto
            {
                NameUsage = (NameUsageType)_random.Next(Enum.GetValues(typeof(NameUsageType)).Length), // Random NameUsageType
                Family = lastName,
                GivenNames = [firstName],
                Gender = _genders[_random.Next(_genders.Count)],
                BirthDate = GenerateRandomBirthDate(),
                Active = _random.NextDouble() > 0.1 // 90% chance the patient is active
            };

            return patient;
        }

        private static DateTime GenerateRandomBirthDate()
        {
            var today = DateTime.Today;
            var minAge = 0;  // minimum age for a patient
            var maxAge = 80;  // maximum age for a patient

            var minDate = today.AddYears(-maxAge);
            var maxDate = today.AddYears(-minAge);

            // Randomly generate a birthdate within the specified range
            var range = maxDate - minDate;
            var randomDays = _random.Next(range.Days);

            return minDate.AddDays(randomDays);
        }
    }
}
