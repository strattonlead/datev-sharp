using CsvHelper.Configuration.Attributes;
using System;

namespace DatevSharp.EXIF.Models
{
    public class DebitorKreditor
    {
        [Index(0)]
        public string Konto { get; set; } = string.Empty;

        [Index(1)]
        public string NameUnternehmen { get; set; } = string.Empty;

        [Index(4)]
        public string NameNatuerlichePerson { get; set; } = string.Empty;

        [Index(5)]
        public string Vorname { get; set; } = string.Empty;

        [Index(7)]
        public string Kurzbezeichnung { get; set; } = string.Empty;

        [Index(9)]
        public string EULand { get; set; } = string.Empty;

        [Index(10)]
        public string EUUStId { get; set; } = string.Empty;

        [Index(15)]
        public string Straße { get; set; } = string.Empty;

        [Index(16)]
        public string Postfach { get; set; } = string.Empty;

        [Index(17)]
        public string PLZ { get; set; } = string.Empty;

        [Index(18)]
        public string Ort { get; set; } = string.Empty;

        [Index(19)]
        public string Land { get; set; } = string.Empty;

        [Index(33)]
        public string Email { get; set; } = string.Empty;

        [Index(41)]
        public string Bankleitzahl1 { get; set; } = string.Empty;

        [Index(43)]
        public string Kontonummer1 { get; set; } = string.Empty;

        [Index(45)]
        public string IBAN1 { get; set; } = string.Empty;

        [Index(47)]
        public string SWIFT1 { get; set; } = string.Empty;

        [Index(49)]
        public string Hauptbankverbindung { get; set; } = string.Empty;

        public override string ToString() => $"{Konto} {Kurzbezeichnung} {Ort} ({Land})";
    }
}
