using GIF.Core.Models;
using GIF.Core.Settings;
using Microsoft.Extensions.Options;

namespace GIF.Core.Services
{
    public class Ds4FileService
    {
        readonly CsvFileService _csvFileService;
        readonly Ds4Setting _ds4Setting;
        readonly MasterData _masterData;
        public Ds4FileService(CsvFileService csvFileService
            , IOptions<Ds4Setting> ds4Setting
            , IOptions<MasterData> masterData)
        {
            _csvFileService = csvFileService;
            _masterData = masterData.Value;
            _ds4Setting = ds4Setting.Value;
        }

        public async Task GenerateBasicFileAsync(Ds4Request request)
        {
            var records = GenerateBasicData(request.PostCode, request.FromHouseNumber, request.ToHouseNumber);
            var path = Path.Combine(_ds4Setting.DiskPath, $"{_masterData.Builder}-{_masterData.Co}-{DateTime.Now.ToString("yyyy-MM-dd-HHmmss")}.csv");
            await _csvFileService.WriteAsync(path, records);
        }

        private IEnumerable<Ds4Model> GenerateBasicData(string postCode, int fromNumber, int toNumber)
        {
            for (int i = fromNumber; i <= toNumber; i++)
            {
                yield return new Ds4Model
                {
                    DhId = "",
                    SearchCode = $"{postCode.ToUpper()}|{i}||",
                    PostalCode = postCode.ToUpper(),
                    HouseNumber = i,
                    HouseNumberExtension = "",
                    Room = "",
                    StreetName = "Luong Son",
                    City = "Nha Trang",
                    CustomerPermission = "Ja",
                    HPPlasticStart = "",
                    HPPlasticPlandate = "",
                    HPPlasticCompleted = "",
                    HCPlandate = "",
                    HCCustomerAppointment = "",
                    HCCompleted = "",
                    LC = _masterData.LC,
                    WP = _masterData.WP,
                    Cabinet1v = i * 10,
                    Tray1v = i,
                    Position1v = i,
                    Cabinet2v = null,
                    Tray2v = null,
                    Position2v = null,
                    EVPCode = "GIESEN-01-EVP01-04",
                    Impedance1v = null,
                    Impedance2v = null,
                    Location = "MTK",
                    FTUType = "C_FTU",
                    DeliveryStatus = 1,
                    DeliveryStatusReason = "",
                    Comment = ""
                };
            }
        }
    }
}
