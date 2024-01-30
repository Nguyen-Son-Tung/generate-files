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
            var records = GenerateFromPattern(request);
            var path = Path.Combine(_ds4Setting.DiskPath, $"{_masterData.Builder}-{_masterData.Co}-{DateTime.Now.ToString("yyyy-MM-dd-HHmmss")}.csv");
            await _csvFileService.WriteAsync(path, records);
        }

        //private IEnumerable<Ds4Model> GenerateBasicData(string postCode, int fromNumber, int toNumber)
        //{
        //    for (int i = fromNumber; i <= toNumber; i++)
        //    {
        //        yield return new Ds4Model
        //        {
        //            DhId = "",
        //            SearchCode = $"{postCode.ToUpper()}|{i}||",
        //            PostalCode = postCode.ToUpper(),
        //            HouseNumber = i,
        //            HouseNumberExtension = "",
        //            Room = "",
        //            StreetName = "Luong Son",
        //            City = "Nha Trang",
        //            CustomerPermission = "Ja",
        //            HPPlasticStart = "02-01-2024",
        //            HPPlasticPlandate = "02-01-2024",
        //            HPPlasticCompleted = "",
        //            HCPlandate = "",
        //            HCCustomerAppointment = "",
        //            HCCompleted = "",
        //            LC = _masterData.LC,
        //            WP = _masterData.WP,
        //            Cabinet1v = i * 10,
        //            Tray1v = i * 10,
        //            Position1v = i,
        //            Cabinet2v = null,
        //            Tray2v = null,
        //            Position2v = null,
        //            EVPCode = "GIESEN-01-EVP01-04",
        //            Impedance1v = null,
        //            Impedance2v = null,
        //            Location = "MTK",
        //            FTUType = "C_FTU",
        //            DeliveryStatus = 1,
        //            DeliveryStatusReason = "",
        //            Comment = ""
        //        };
        //    }
        //}

        private IEnumerable<Ds4Model> GenerateFromPattern(Ds4Request ds4request)
        {
            var records = _csvFileService.Read<Ds4Model>(_ds4Setting.PatternFilePath);
            var patternRecord = records[0];
            for (var i = ds4request.RoomStartNumber; i <= ds4request.RoomEndNumber; i++)
            {
                var temp = patternRecord.Clone();
                temp.PostalCode = ds4request.PostCode.ToUpper();
                temp.HouseNumber = ds4request.HouseNumber;
                temp.HouseNumberExtension = ds4request.HouseExtension.ToUpper();
                temp.Room = (i + "").ToUpper();
                temp.LC = _masterData.LC;
                temp.WP = _masterData.WP;
                temp.Cabinet1v = i * 10;
                temp.Tray1v = i * ds4request.Step;
                temp.Position1v = i;
                temp.SearchCode = $"{ds4request.PostCode}|{temp.HouseNumber}|{temp.HouseNumberExtension}|{temp.Room}".ToUpper();
                yield return temp;
            }
        }
    }
}
