using CsvHelper.Configuration.Attributes;
using System;

namespace DatevSharp.EXIF.Models
{
    /// <summary>
    /// Repräsentiert eine Zeile im DATEV-Exportformat „Buchungsstapel“.
    /// </summary>
    public class Buchung
    {
        [Index(0)]
        public decimal Umsatz { get; set; }

        [Index(1)]
        public string SollHabenKennzeichen { get; set; } = "\"S\"";

        [Index(2)]
        public string Waehrung { get; set; } = "\"EUR\"";

        [Index(3)]
        public string Kurs { get; set; } = "";

        [Index(4)]
        public string BasisUmsatz { get; set; } = "";

        [Index(5)]
        public string WKZBasisUmsatz { get; set; } = "";

        [Index(6)]
        public string Konto { get; set; } = "";

        [Index(7)]
        public string Gegenkonto { get; set; } = "";

        [Index(8)]
        public string BUSchluessel { get; set; } = "";

        [Index(9)]
        public string Belegdatum { get; set; } = "";

        [Index(10)]
        public string Belegfeld1 { get; set; } = "";

        [Index(11)]
        public string Belegfeld2 { get; set; } = "";

        [Index(12)]
        public string Skonto { get; set; } = "";

        [Index(13)]
        public string Buchungstext { get; set; } = "";

        [Index(14)]
        public string Postensperre { get; set; } = "";

        [Index(15)]
        public string DiverseAdressnummer { get; set; } = "";

        // ... Die Liste geht bis Index 125+
        // Für den Start genügt ein minimaler, valider Satz

        public override string ToString() =>
            $"{Umsatz} {SollHabenKennzeichen} {Konto}->{Gegenkonto} BU: {BUSchluessel}";
    }
}
