using GIF.Core.Models;
using GIF.Core.Settings;
using Microsoft.Extensions.Options;

namespace GIF.Core.Services
{
    public class Ds0FileService
    {
        readonly CsvFileService _csvFileService;
        readonly Ds0Setting _ds0Setting;
        public Ds0FileService(CsvFileService csvFileService
            , IOptions<Ds0Setting> ds0Setting)
        {
            _csvFileService = csvFileService;
            _ds0Setting = ds0Setting.Value;
        }
        public async Task GenerateFileAsync(Ds0Request ds0Request)
        {
            var records = GenerateFromPattern(ds0Request);
            var dateFormat = "yyyy-MM-dd-HH-mm-ss";
            var path = Path.Combine(_ds0Setting.DiskPath, $"DS0-{ds0Request.GetIdentity()}-{DateTime.Now.ToString(dateFormat)}.csv");
            await _csvFileService.WriteAsync(path, records);
        }

        private IEnumerable<Ds0Model> GenerateFromPattern(Ds0Request ds0Request)
        {
            var records = _csvFileService.Read<Ds0Model>(_ds0Setting.PatternFilePath);
            var patternRecord = records[0];
            for (var i = ds0Request.RoomStartNumber; i <= ds0Request.RoomEndNumber; i++)
            {
                var temp = patternRecord.Clone();
                temp.Postcode = ds0Request.PostCode.ToUpper();
                temp.Huisnummer = ds0Request.HouseNumber;
                temp.Toevoeging = ds0Request.HouseExtension.ToUpper();
                temp.Kamer = (i + "").ToUpper();
                temp.Gebied = ds0Request.AreaCode;
                temp.Bouwopdracht = ds0Request.CoCode;
                yield return temp;
            }
        }
    }
}
