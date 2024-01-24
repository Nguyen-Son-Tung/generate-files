using GIF.Core.Models;
using GIF.Core.Settings;
using Microsoft.Extensions.Options;

namespace GIF.Core.Services
{
    public class Ds0FileService
    {
        readonly CsvFileService _csvFileService;
        readonly Ds0Setting _ds0Setting;
        readonly MasterData _masterData;
        public Ds0FileService(CsvFileService csvFileService
            , IOptions<Ds0Setting> ds0Setting
            , IOptions<MasterData> masterData)
        {
            _csvFileService = csvFileService;
            _ds0Setting = ds0Setting.Value;
            _masterData = masterData.Value;

        }
        public async Task GenerateFileAsync(Ds0Request ds0Request, int version)
        {
            var records = new List<Ds0Model>();
            if (version == 1)
                records.AddRange(GenerateNewAddressData(ds0Request.PostCode, ds0Request.FromHouseNumber, ds0Request.ToHouseNumber));
            else if (version == 2)
                records.AddRange(GenerateFromPattern(ds0Request));
            else
                throw new NotImplementedException("Version has not implemented yet.");

            var path = Path.Combine(_ds0Setting.DiskPath, $"DS0-{ds0Request.PostCode.ToUpper()}-{ds0Request.ToHouseNumber}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.csv");
            await _csvFileService.WriteAsync(path, records);
        }
        private IEnumerable<Ds0Model> GenerateNewAddressData(string postCode, int fromNumber, int toNumber)
        {
            for (int i = fromNumber;  i <= toNumber; i++)
            {
                yield return new Ds0Model
                {
                    Postcode = postCode.ToUpper(),
                    Straat = "Luong Son",
                    Huisnummer = i.ToString(),
                    Toevoeging = "",
                    Kamer = "",
                    Plaats = "Khanh Hoa",
                    Buurtschap = "Nha Trang",
                    Gemeente = "Vinh Luong",
                    Gebied = _masterData.Area,
                    Bouwopdracht = _masterData.Co,
                    AdresType = "FttH-Wit",
                    Opmerking = "Opmerking",
                    BagId = "1710000000000000",
                    Gebruiksdoel = "Gebruiksdoel",
                    RawXCoordinate = "130128",
                    RawYCoordinate = "417134",
                    Geactiveerd = "1",
                    DeactivatieCommentaar = "DeactivatieCommentaar",
                    Orderbaar = "1",
                    NotOrderbaarCommentaar = "",
                    Buitengebied = "Buitengebied",
                    TypeBouw = "Hoogbouw",
                    Collectiviteit = "WOCO",
                    CollectiviteitOpmerking = "CollectiviteitOpmerking"
                };
            }
        }

        private IEnumerable<Ds0Model> GenerateFromPattern(Ds0Request ds0Request)
        {
            var records = _csvFileService.Read<Ds0Model>(_ds0Setting.PatternFilePath);
            var patternRecord = records.First();
            for (var i = ds0Request.FromHouseNumber; i <= ds0Request.ToHouseNumber; i++)
            {
                var temp = patternRecord.Clone();
                temp.Postcode = ds0Request.PostCode.ToUpper();
                temp.Huisnummer = i + "";
                temp.Toevoeging = temp.Toevoeging == "{houseNumberExt}" ? null : ds0Request.HouseNumberExt;
                temp.Kamer = temp.Kamer == "{room}" ? null : ds0Request.Room;
                temp.Gebied = _masterData.Area;
                temp.Bouwopdracht = _masterData.Co;
                yield return temp;
            }
        }
    }
}
