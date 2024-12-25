using System.Text;
using System.Text.Json;

namespace PatientsCretionConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serverUrl = "http://localhost:8080/api/patients/add";
            var numberOfPatients = 100;

            using HttpClient httpClient = new();

            var serializationOptions = new JsonSerializerOptions();
            //serializationOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            for (int i = 0; i < numberOfPatients; i++)
            {
                var success = await TrySendRandomPatientAsync(
                    httpClient,
                    serverUrl,
                    serializationOptions);

                if (!success)
                    break;
            }

            Console.ReadLine();
        }

        private static async Task<bool> TrySendRandomPatientAsync(
            HttpClient httpClient,
            string apiUrl,
            JsonSerializerOptions jsonOptions)
        {
            try
            {
                var patient = RandomPatientGenerator.GenerateRandomPatient();

                string patientJson = JsonSerializer.Serialize(patient, jsonOptions);
                StringContent content = new(patientJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    Console.WriteLine($"Patient sent successfully ({responseContent})");
                else
                    Console.WriteLine($"Failed to send patient. Status Code: {response.StatusCode} ({responseContent})");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Patient data sendint error! ({ex})");
                return false;
            }
        }
    }
}
