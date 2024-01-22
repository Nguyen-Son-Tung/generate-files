using CsvHelper.Configuration.Attributes;

namespace GIF.Core.Models
{
    public class Ds0Model
    {
        [Name("Postcode"), Index(1)]
        public string? Postcode { get; set; }

        [Name("Straat"), Index(2)]
        public string? Straat { get; set; }

        [Name("Huisnummer"), Index(3)]
        public int Huisnummer { get; set; }

        [Name("Toevoeging"), Index(4)]
        public string? Toevoeging { get; set; }

        [Name("Kamer"), Index(5)]
        public string? Kamer { get; set; }

        [Name("Plaats"), Index(6)]
        public string? Plaats { get; set; }

        [Name("Buurtschap"), Index(7)]
        public string? Buurtschap { get; set; }

        [Name("Gemeente"), Index(8)]
        public string? Gemeente { get; set; }

        [Name("Gebied"), Index(9)]
        public string? Gebied { get; set; }

        [Name("Bouwopdracht"), Index(10)]
        public string? Bouwopdracht { get; set; }

        [Name("Adres type"), Index(11)]
        public string? AdresType { get; set; }

        [Name("Opmerking"), Index(12)]
        public string? Opmerking { get; set; }

        [Name("Bag Id"), Index(13)]
        public string? BagId { get; set; }

        [Name("Gebruiksdoel"), Index(14)]
        public string? Gebruiksdoel { get; set; }
        [Name("X"), Index(15)]

        public string? RawXCoordinate { get; set; }
        [Name("Y"), Index(16)]
        public string? RawYCoordinate { get; set; }

        [Name("Geactiveerd"), Index(17)]
        public int? Geactiveerd { get; set; }

        [Name("Deactivatie commentaar"), Index(18)]
        public string? DeactivatieCommentaar { get; set; }

        [Name("Orderbaar"), Index(19)]
        public int? Orderbaar { get; set; }

        [Name("Niet Orderbaar Commentaar"), Index(20)]
        public string? NotOrderbaarCommentaar { get; set; }

        [Name("Kern/Buitengebied"), Index(21)]
        public string? Buitengebied { get; set; }

        [Name("Type bouw"), Index(22)]
        public string? TypeBouw { get; set; }

        [Name("Collectiviteit"), Index(23)]
        public string? Collectiviteit { get; set; }

        [Name("Collectiviteit opmerking"), Index(24)]
        public string? CollectiviteitOpmerking { get; set; }
    }

    public class Ds0Request
    {
        public string PostCode { get; set; } = default!;
        public int FromHouseNumber { get; set; }
        public int ToHouseNumber { get; set; }
    }
}
