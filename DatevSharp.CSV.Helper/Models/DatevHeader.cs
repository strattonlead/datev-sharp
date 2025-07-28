using DatevSharp.CSV.Helper;
using System;

namespace CreateIf.Datev.Models
{
    /// <summary>
    /// Repräsentiert die Kopfzeile einer DATEV-CSV-Datei (EXTF).
    /// </summary>
    public class DatevHeader
    {
        [DatevPosition(0)]
        public string Kennzeichen { get; set; } = "\"EXTF\""; // oder "DTVF"

        [DatevPosition(1)]
        public int Versionsnummer { get; set; } = 700;

        [DatevPosition(2)]
        public int Formatkategorie { get; set; } // z. B. 21 für Buchungsstapel

        [DatevPosition(3)]
        public string Formatname { get; set; } = "\"Buchungsstapel\"";

        [DatevPosition(4)]
        public int Formatversion { get; set; } = 13;

        [DatevPosition(5)]
        [DatevFormat("yyyyMMddHHmmssfff")]
        public DateTime ErzeugtAm { get; set; } = DateTime.Now;

        [DatevPosition(6)]
        public string Importiert { get; set; }

        [DatevPosition(7)]
        public string Herkunft { get; set; } = "\"RE\"";

        [DatevPosition(8)]
        public string ExportiertVon { get; set; } = "\"CreateIf\"";

        [DatevPosition(9)]
        public string ImportiertVon { get; set; } = "\"\"";

        [DatevPosition(10)]
        public int Beraternummer { get; set; }

        [DatevPosition(11)]
        public int Mandantennummer { get; set; }

        [DatevPosition(12)]
        [DatevFormat("yyyyMMdd")]
        public DateTime Wirtschaftsjahresbeginn { get; set; }

        [DatevPosition(13)]
        public int Sachkontenlaenge { get; set; } = 4;

        [DatevPosition(14)]
        [DatevFormat("yyyyMMdd")]
        public DateTime DatumVon { get; set; }

        [DatevPosition(15)]
        [DatevFormat("yyyyMMdd")]
        public DateTime DatumBis { get; set; }

        [DatevPosition(16)]
        public string Bezeichnung { get; set; } = "\"Buchungsstapel\"";

        [DatevPosition(17)]
        public string Diktatkürzel { get; set; } = "\"WD\"";

        [DatevPosition(18)]
        public int Buchungstyp { get; set; } = 1;

        [DatevPosition(19)]
        public int Rechnungslegungszweck { get; set; } = 0;

        [DatevPosition(20)]
        public int Festschreibung { get; set; } = 0;

        [DatevPosition(21)]
        public string Waehrung { get; set; } = "\"EUR\"";

        [DatevPosition(22)]
        public string Reserviert1 { get; set; } = "";

        [DatevPosition(23)]
        public string Derivatskennzeichen { get; set; } = "\"\"";

        [DatevPosition(24)]
        public string Reserviert2 { get; set; } = "";

        [DatevPosition(25)]
        public string Reserviert3 { get; set; } = "";


        [DatevPosition(26)]
        public string Sachkontenrahmen { get; set; } = "\"03\""; // z. B. 03 = SKR03

        [DatevPosition(27)]
        public string BranchenloesungId { get; set; } = ""; // optional 4-stellig

        [DatevPosition(28)]
        public string Reserviert4 { get; set; }

        [DatevPosition(29)]
        public string Reserviert5 { get; set; } = "";

        [DatevPosition(30)]
        public string Anwendungsinformation { get; set; } = "\"\""; // max. 16 Zeichen
    }
}
