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
    }
}
