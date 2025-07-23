using CsvHelper.Configuration.Attributes;
using System;

namespace CreateIf.Datev.Models
{
    /// <summary>
    /// Repräsentiert die Kopfzeile einer DATEV-CSV-Datei (EXTF).
    /// </summary>
    public class DatevHeader
    {
        [Index(0)]
        public string Kennzeichen { get; set; } = "\"EXTF\""; // oder "DTVF"

        [Index(1)]
        public int Versionsnummer { get; set; } = 700;

        [Index(2)]
        public int Formatkategorie { get; set; } // z. B. 21 für Buchungsstapel

        [Index(3)]
        public string Formatname { get; set; } = "\"Buchungsstapel\"";

        [Index(4)]
        public int Formatversion { get; set; } = 13;

        [Index(5)]
        public string ErzeugtAm { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmssfff");

        [Index(6)]
        public string Importiert { get; set; } = "";

        [Index(7)]
        public string Herkunft { get; set; } = "\"RE\"";

        [Index(8)]
        public string ExportiertVon { get; set; } = "\"CreateIf\"";

        [Index(9)]
        public string ImportiertVon { get; set; } = "\"\"";

        [Index(10)]
        public int Beraternummer { get; set; }

        [Index(11)]
        public int Mandantennummer { get; set; }

        [Index(12)]
        public string Wirtschaftsjahresbeginn { get; set; } = "20250101"; // Format: yyyyMMdd

        [Index(13)]
        public int Sachkontenlaenge { get; set; } = 4;

        [Index(14)]
        public string DatumVon { get; set; } = ""; // yyyyMMdd optional

        [Index(15)]
        public string DatumBis { get; set; } = ""; // yyyyMMdd optional

        [Index(16)]
        public string Bezeichnung { get; set; } = "\"Buchungsstapel\"";

        [Index(17)]
        public string Diktatkürzel { get; set; } = "\"WD\"";

        [Index(18)]
        public int Buchungstyp { get; set; } = 1;

        [Index(19)]
        public int Rechnungslegungszweck { get; set; } = 0;

        [Index(20)]
        public int Festschreibung { get; set; } = 0;

        [Index(21)]
        public string Waehrung { get; set; } = "\"EUR\"";

        [Index(22)]
        public string Reserviert22 { get; set; } = "";

        [Index(23)]
        public string Derivatskennzeichen { get; set; } = "\"\"";

        [Index(24)]
        public string Reserviert24 { get; set; } = "";

        [Index(25)]
        public string Reserviert25 { get; set; } = "";

        [Index(26)]
        public string Reserviert26 { get; set; } = "";

        [Index(27)]
        public string Sachkontenrahmen { get; set; } = "\"03\""; // z. B. 03 = SKR03

        [Index(28)]
        public string BranchenloesungId { get; set; } = ""; // optional 4-stellig

        [Index(29)]
        public string Reserviert29 { get; set; } = "";

        [Index(30)]
        public string Anwendungsinformation { get; set; } = "\"\""; // max. 16 Zeichen
    }
}
