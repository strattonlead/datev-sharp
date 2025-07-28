//using DatevSharp.CSV.Helper;

//namespace DatevSharp.EXIF.Models
//{
//    /// <summary>
//    /// Repräsentiert eine Zeile im DATEV-Exportformat „Buchungsstapel“.
//    /// </summary>
//    public class Buchung
//    {
//        [DatevPosition(0)]
//        public decimal Umsatz { get; set; }

//        [DatevPosition(1)]
//        public string SollHabenKennzeichen { get; set; } = "\"S\"";

//        [DatevPosition(2)]
//        public string Waehrung { get; set; } = "\"EUR\"";

//        [DatevPosition(3)]
//        public string Kurs { get; set; } = "";

//        [DatevPosition(4)]
//        public string BasisUmsatz { get; set; } = "";

//        [DatevPosition(5)]
//        public string WKZBasisUmsatz { get; set; } = "";

//        [DatevPosition(6)]
//        public string Konto { get; set; } = "";

//        [DatevPosition(7)]
//        public string Gegenkonto { get; set; } = "";

//        [DatevPosition(8)]
//        public string BUSchluessel { get; set; } = "";

//        [DatevPosition(9)]
//        public string Belegdatum { get; set; } = "";

//        [DatevPosition(10)]
//        public string Belegfeld1 { get; set; } = "";

//        [DatevPosition(11)]
//        public string Belegfeld2 { get; set; } = "";

//        [DatevPosition(12)]
//        public string Skonto { get; set; } = "";

//        [DatevPosition(13)]
//        public string Buchungstext { get; set; } = "";

//        [DatevPosition(14)]
//        public string Postensperre { get; set; } = "";

//        [DatevPosition(15)]
//        public string DiverseAdressnummer { get; set; } = "";

//        // ... Die Liste geht bis DatevPosition 125+
//        // Für den Start genügt ein minimaler, valider Satz

//        public override string ToString() =>
//            $"{Umsatz} {SollHabenKennzeichen} {Konto}->{Gegenkonto} BU: {BUSchluessel}";
//    }
//}
