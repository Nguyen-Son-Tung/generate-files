using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace GIF.Core.Services
{
    public class CsvFileService
    {
        public async Task WriteAsync<T>(string path, IEnumerable<T> records) where T : class
        {
            using var writer = new StreamWriter(path);
            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" });
            csv.WriteHeader<T>();
            await csv.NextRecordAsync();
            csv.WriteRecords(records);
        }

        public T[] Read<T>(string path) where T : class
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<T>().ToArray();
                return records;
            }
        }
    }
}
