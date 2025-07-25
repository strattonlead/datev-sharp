using DatevSharp.CSV.Helper;

namespace DatevSharp.EXIF.Models
{
    public class DebitorKreditor
    {
        [DatevPosition(0)]
        public string Konto { get; set; } = string.Empty;

        [DatevPosition(1)]
        public string NameUnternehmen { get; set; } = string.Empty;

        [DatevPosition(4)]
        public string NameNatuerlichePerson { get; set; } = string.Empty;

        [DatevPosition(5)]
        public string Vorname { get; set; } = string.Empty;

        [DatevPosition(7)]
        public string Kurzbezeichnung { get; set; } = string.Empty;

        [DatevPosition(9)]
        public string EULand { get; set; } = string.Empty;

        [DatevPosition(10)]
        public string EUUStId { get; set; } = string.Empty;

        [DatevPosition(15)]
        public string Straße { get; set; } = string.Empty;

        [DatevPosition(16)]
        public string Postfach { get; set; } = string.Empty;

        [DatevPosition(17)]
        public string PLZ { get; set; } = string.Empty;

        [DatevPosition(18)]
        public string Ort { get; set; } = string.Empty;

        [DatevPosition(19)]
        public string Land { get; set; } = string.Empty;

        [DatevPosition(33)]
        public string Email { get; set; } = string.Empty;

        [DatevPosition(41)]
        public string Bankleitzahl1 { get; set; } = string.Empty;

        [DatevPosition(43)]
        public string Kontonummer1 { get; set; } = string.Empty;

        [DatevPosition(45)]
        public string IBAN1 { get; set; } = string.Empty;

        [DatevPosition(47)]
        public string SWIFT1 { get; set; } = string.Empty;

        [DatevPosition(49)]
        public string Hauptbankverbindung { get; set; } = string.Empty;

        public override string ToString() => $"{Konto} {Kurzbezeichnung} {Ort} ({Land})";
    }
}
