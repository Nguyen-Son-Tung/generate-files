using GIF.Core.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GIF.Core.Models
{
    public class Ds4Model
    {
        public string? DhId { get; set; }
        public string? SearchCode { get; set; }
        public string? PostalCode { get; set; }
        public int? HouseNumber { get; set; }
        public string? HouseNumberExtension { get; set; }
        public string? Room { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? CustomerPermission { get; set; }
        public string? HPPlasticStart { get; set; }
        public string? HPPlasticPlandate { get; set; }
        public string? HPPlasticCompleted { get; set; }
        public string? HCPlandate { get; set; }
        public string? HCCustomerAppointment { get; set; }
        public string? HCCompleted { get; set; }
        public string? LC { get; set; }
        public string? WP { get; set; }
        public int? Cabinet1v { get; set; }
        public int? Tray1v { get; set; }
        public double? Position1v { get; set; }
        public int? Cabinet2v { get; set; }
        public int? Tray2v { get; set; }
        public double? Position2v { get; set; }
        public string? EVPCode { get; set; }
        public double? Impedance1v { get; set; }
        public double? Impedance2v { get; set; }
        public string? Location { get; set; }
        public string? FTUType { get; set; }
        public int? DeliveryStatus { get; set; }
        public string? DeliveryStatusReason { get; set; }
        public string? Comment { get; set; }

        public Ds4Model Clone()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Ds4Model>(serialized) ?? throw new InvalidCastException("Cannot clone object.");
        }
    }

    public class Ds4Request
    {
        [Required]
        public string PostCode { get; set; } = "1212TD";
        [Required]
        public int HouseNumber { get; set; } = 1;
        [Required]
        public string HouseExtension { get; set; } = "A";
        [Required]
        public int RoomStartNumber { get; set; } = 1;
        [Required]
        public int RoomEndNumber { get; set; } = 100;
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
        public Ds4FileTemplate? Template { get; set; }
        public int Step { get; set; } = 1;
    }
}
