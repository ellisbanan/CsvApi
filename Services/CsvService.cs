using CsvApi.Models;

namespace CsvApi.Services
{
    public interface ICsvService
    {
        List<Person> ReadCsv(string filePath);
    }

    public class CsvService : ICsvService
    {
        /// <summary>
        /// Reads a CSV file and returns a list of Person objects.
        /// </summary>
        /// <param name="filePath">Path to the CSV file</param>
        /// <returns>List of persons parsed from the CSV file</returns>
        /// <exception cref="FileNotFoundException">Thrown if file does not exist or it is not a CSV file</exception>
        /// <exception cref="InvalidDataException">Thrown if CSV content is invalid</exception>
        public List<Person> ReadCsv(string filePath)
        {
            var people = new List<Person>();

            // Only allow csv files
            if (!File.Exists(filePath) || !Path.GetExtension(filePath).Equals(".csv"))
            {
                throw new FileNotFoundException("CSV file not found or wrong file type.", filePath);
            }

            var lines = File.ReadAllLines(filePath);

            // Return empty list if no data
            if (lines.Length == 0)
            {
                return people; 
            }

            foreach (string line in lines)
            {
                var values = line.Split(';');

                // Expecting exactly 4 columns: Id, Name, Age, Email
                if (values.Length != 4) throw new InvalidDataException("Invalid CSV format");

                // Validate and parse data types
                if (int.TryParse(values[0], out int id) && int.TryParse(values[2], out int age))
                {
                    var person = new Person
                    {
                        Id = id,
                        Name = values[1],
                        Age = age,
                        Email = values[3]
                    };
                    people.Add(person);
                }
                else
                {
                    throw new InvalidDataException("Invalid data types in CSV");
                }
            }

            return people;
        }
    }
}
