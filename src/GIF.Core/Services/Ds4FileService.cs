using GIF.Core.Models;
using GIF.Core.Settings;
using Microsoft.Extensions.Options;

namespace GIF.Core.Services
{
    public class Ds4FileService
    {
        readonly CsvFileService _csvFileService;
        readonly Ds4Setting _ds4Setting;
        public Ds4FileService(CsvFileService csvFileService
            , IOptions<Ds4Setting> ds4Setting)
        {
            _csvFileService = csvFileService;
            _ds4Setting = ds4Setting.Value;
        }

        public async Task GenerateBasicFileAsync(Ds4Request request)
        {
            var records = GenerateFromPattern(request);
            var dateFormat = "yyyy-MM-dd-HHmmss";
            var path = Path.Combine(_ds4Setting.DiskPath, $"{request.Builder}-{request.CoCode}-{DateTime.Now.ToString(dateFormat)}.csv");
            await _csvFileService.WriteAsync(path, records);
        }

        private IEnumerable<Ds4Model> GenerateFromPattern(Ds4Request ds4request)
        {
            string patternPath = _ds4Setting.PatternFilePath;
            string patternFile = ds4request.Template switch
            {
                Enums.Ds4FileTemplate.ODF => Path.Combine(patternPath, "Dataset4_ODF.csv"),
                Enums.Ds4FileTemplate.HPP => Path.Combine(patternPath, "Dataset4_HPP.csv"),
                Enums.Ds4FileTemplate.HPP_D2 => Path.Combine(patternPath, "Dataset4_HPP_D2.csv"),
                Enums.Ds4FileTemplate.Delivery0 => Path.Combine(patternPath, "Dataset4_Delivery0.csv"),
                _ => Path.Combine(patternPath, "Dataset4_ODF.csv"),
            };
            var records = _csvFileService.Read<Ds4Model>(patternFile);
            var patternRecord = records[0];
            for (var i = ds4request.RoomStartNumber; i <= ds4request.RoomEndNumber; i++)
            {
                var temp = patternRecord.Clone();
                temp.PostalCode = ds4request.PostCode.ToUpper();
                temp.HouseNumber = ds4request.HouseNumber;
                temp.HouseNumberExtension = ds4request.HouseExtension.ToUpper();
                temp.Room = (i + "").ToUpper();
                temp.LC = ds4request.LC;
                temp.WP = ds4request.WP;
                temp.Cabinet1v = i * 10;
                temp.Tray1v = i * ds4request.Step;
                temp.Position1v = i;
                temp.SearchCode = $"{ds4request.PostCode}|{temp.HouseNumber}|{temp.HouseNumberExtension}|{temp.Room}".ToUpper();
                temp.HPPlasticPlandate = FillDateValue(temp.HPPlasticPlandate);
                temp.HPPlasticStart = FillDateValue(temp.HPPlasticStart);
                temp.HPPlasticCompleted = FillDateValue(temp.HPPlasticCompleted);
                temp.HCPlandate = FillDateValue(temp.HCPlandate);
                temp.HCCustomerAppointment = FillDateValue(temp.HCCustomerAppointment);
                temp.HCCompleted = FillDateValue(temp.HCCompleted);
                yield return temp;
            }
        }

        private string? FillDateValue(string? value) 
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return DateTime.Now.AddDays(-2).ToString("dd-MM-yyyy");
        }
    }
}
