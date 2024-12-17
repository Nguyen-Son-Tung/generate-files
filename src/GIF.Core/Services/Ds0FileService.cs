using GIF.Core.Models;
using GIF.Core.Settings;
using Microsoft.Extensions.Options;

namespace GIF.Core.Services
{
    public class Ds0FileService(CsvFileService csvFileService
            , IOptions<Ds0Setting> ds0Setting)
    {
        readonly CsvFileService _csvFileService = csvFileService;
        readonly Ds0Setting _ds0Setting = ds0Setting.Value;

        public async Task GenerateFileAsync(Ds0Request ds0Request)
        {
            var records = GenerateFromPattern(ds0Request);
            var dateFormat = "yyyy-MM-dd-HH-mm-ss";
            var path = Path.Combine(_ds0Setting.DiskPath, $"DS0-{ds0Request.GetIdentity()}-{DateTime.Now.ToString(dateFormat)}.csv");
            await _csvFileService.WriteAsync(path, records);
        }

        public async Task<string> GenerateAdmFileAsync(Ds0AdmRequest ds0Request)
        {
            var result = GenerateFromAdmPattern(ds0Request);
            var dateFormat = "yyyy-MM-dd-HH-mm-ss";
            var path = Path.Combine(_ds0Setting.DiskPath, $"DS0-{ds0Request.GetIdentity()}-{DateTime.Now.ToString(dateFormat)}-ADM.csv");
            await _csvFileService.WriteAsync(path, result.Ds0AdmModels);
            return result.InsertSql;
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

        private Ds0AdmResponse GenerateFromAdmPattern(Ds0AdmRequest ds0Request)
        {
            var list = new List<Ds0AdmModel>();
            var records = _csvFileService.Read<Ds0AdmModel>("PatternFiles\\Dataset0ADM.csv");
            var patternRecord = records[0];
            for (var i = ds0Request.RoomStartNumber; i <= ds0Request.RoomEndNumber; i++)
            {
                var temp = patternRecord.Clone();
                temp.AdmAddressID = GenerateAdmAddressId(ds0Request.HouseNumber, ds0Request.HouseExtension, i);
                temp.Postcode = ds0Request.PostCode.ToUpper();
                temp.Huisnummer = ds0Request.HouseNumber;
                temp.Toevoeging = ds0Request.HouseExtension.ToUpper();
                temp.Kamer = (i + "").ToUpper();
                temp.Gebied = ds0Request.AreaCode;
                temp.Bouwopdracht = ds0Request.CoCode;
                list.Add(temp);
            }

            if (ds0Request.GenSqlMock)
            {
                string query = "INSERT INTO AdmMockTest (AddressId, PostalCode, HouseNumber, HouseNumberExtension, Room, City, Street, X, Y, Status) VALUES ";
                List<string> insertValues = [];
                foreach (var record in list)
                {
                    var insert = $"('{record.AdmAddressID}', '{record.Postcode}', {record.Huisnummer}, '{record.Toevoeging}', '{record.Kamer}', '{record.Plaats}', '{record.Straat}', {record.RawXCoordinate}, {record.RawYCoordinate}, 'active')";
                    insertValues.Add(insert);
                }
                query += string.Join(", ", insertValues);

                return new Ds0AdmResponse { Ds0AdmModels = list, InsertSql = query };
            }
            return new Ds0AdmResponse { Ds0AdmModels = list };
        }

        private static string GenerateAdmAddressId(int houseNumber, string houseExt, int room)
        {
            string addressID = $"ADR-NL{houseExt}-ST{houseNumber}-{room.ToString("D3")}";
            return addressID;
        }
    }
}
