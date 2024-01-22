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
        public async Task ExportFileAsync(Ds0Request ds0Request)
        {
            var records = GenerateNewAddressData(ds0Request.PostCode, ds0Request.FromHouseNumber, ds0Request.ToHouseNumber);
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
                    Huisnummer = i,
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
                    Geactiveerd = 1,
                    DeactivatieCommentaar = "DeactivatieCommentaar",
                    Orderbaar = 1,
                    NotOrderbaarCommentaar = "",
                    Buitengebied = "Buitengebied",
                    TypeBouw = "Hoogbouw",
                    Collectiviteit = "WOCO",
                    CollectiviteitOpmerking = "CollectiviteitOpmerking"
                };
            }
        }
    }
}
