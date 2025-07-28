using DatevSharp.CSV.Helper;
using System;

namespace DatevSharp.EXIF.Models
{
    public class Buchungsstapel
    {
        [DatevPosition(0)]
        public decimal Umsatz { get; set; }

        [DatevPosition(1)]
        public string SollHabenKennzeichen { get; set; }

        [DatevPosition(2)]
        public string WkzUmsatz { get; set; }

        [DatevPosition(3)]
        public decimal? Kurs { get; set; }

        [DatevPosition(4)]
        public decimal? BasisUmsatz { get; set; }

        [DatevPosition(5)]
        public string WkzBasisUmsatz { get; set; }

        [DatevPosition(6)]
        public int Konto { get; set; }

        [DatevPosition(7)]
        public int Gegenkonto { get; set; }

        [DatevPosition(8)]
        public string BUSchluessel { get; set; }

        [DatevPosition(9)]
        [DatevFormat("ddMM")]
        public DateTime Belegdatum { get; set; }

        [DatevPosition(10)]
        public string Belegfeld1 { get; set; }

        [DatevPosition(11)]
        public string Belegfeld2 { get; set; }

        [DatevPosition(12)]
        public decimal? Skonto { get; set; }

        [DatevPosition(13)]
        public string Buchungstext { get; set; }

        [DatevPosition(14)]
        public int? Postensperre { get; set; }

        [DatevPosition(15)]
        public string DiverseAdressnummer { get; set; }

        [DatevPosition(16)]
        public int? Geschaeftspartnerbank { get; set; }

        [DatevPosition(17)]
        public int? Sachverhalt { get; set; }

        [DatevPosition(18)]
        public int? Zinssperre { get; set; }

        [DatevPosition(19)]
        public string Beleglink { get; set; }

        [DatevPosition(20)]
        public string BeleginfoArt1 { get; set; }

        [DatevPosition(21)]
        public string BeleginfoInhalt1 { get; set; }

        [DatevPosition(22)]
        public string BeleginfoArt2 { get; set; }

        [DatevPosition(23)]
        public string BeleginfoInhalt2 { get; set; }

        [DatevPosition(24)]
        public string BeleginfoArt3 { get; set; }

        [DatevPosition(25)]
        public string BeleginfoInhalt3 { get; set; }

        [DatevPosition(26)]
        public string BeleginfoArt4 { get; set; }

        [DatevPosition(27)]
        public string BeleginfoInhalt4 { get; set; }

        [DatevPosition(28)]
        public string BeleginfoArt5 { get; set; }

        [DatevPosition(29)]
        public string BeleginfoInhalt5 { get; set; }

        [DatevPosition(30)]
        public string BeleginfoArt6 { get; set; }

        [DatevPosition(31)]
        public string BeleginfoInhalt6 { get; set; }

        [DatevPosition(32)]
        public string BeleginfoArt7 { get; set; }

        [DatevPosition(33)]
        public string BeleginfoInhalt7 { get; set; }

        [DatevPosition(34)]
        public string BeleginfoArt8 { get; set; }

        [DatevPosition(35)]
        public string BeleginfoInhalt8 { get; set; }

        [DatevPosition(36)]
        public string KOST1 { get; set; }

        [DatevPosition(37)]
        public string KOST2 { get; set; }

        [DatevPosition(38)]
        public decimal? KostMenge { get; set; }

        [DatevPosition(39)]
        public string EUUmsatzLandUStId { get; set; }

        [DatevPosition(40)]
        public decimal? EUUmsatzSteuersatz { get; set; }

        [DatevPosition(41)]
        public string AbwVersteuerungsart { get; set; }

        [DatevPosition(42)]
        public int? SachverhaltLL { get; set; }

        [DatevPosition(43)]
        public int? FunktionsergaenzungLL { get; set; }

        [DatevPosition(44)]
        public int? BU49Hauptfunktiontyp { get; set; }

        [DatevPosition(45)]
        public int? BU49Hauptfunktionsnummer { get; set; }

        [DatevPosition(46)]
        public int? BU49Funktionsergaenzung { get; set; }

        [DatevPosition(47)]
        public string ZusatzinformationArt1 { get; set; }

        [DatevPosition(48)]
        public string ZusatzinformationInhalt1 { get; set; }

        [DatevPosition(49)]
        public string ZusatzinformationArt2 { get; set; }

        [DatevPosition(50)]
        public string ZusatzinformationInhalt2 { get; set; }

        [DatevPosition(51)]
        public string ZusatzinformationArt3 { get; set; }

        [DatevPosition(52)]
        public string ZusatzinformationInhalt3 { get; set; }

        [DatevPosition(53)]
        public string ZusatzinformationArt4 { get; set; }

        [DatevPosition(54)]
        public string ZusatzinformationInhalt4 { get; set; }

        [DatevPosition(55)]
        public string ZusatzinformationArt5 { get; set; }

        [DatevPosition(56)]
        public string ZusatzinformationInhalt5 { get; set; }

        [DatevPosition(57)]
        public string ZusatzinformationArt6 { get; set; }

        [DatevPosition(58)]
        public string ZusatzinformationInhalt6 { get; set; }

        [DatevPosition(59)]
        public string ZusatzinformationArt7 { get; set; }

        [DatevPosition(60)]
        public string ZusatzinformationInhalt7 { get; set; }

        [DatevPosition(61)]
        public string ZusatzinformationArt8 { get; set; }

        [DatevPosition(62)]
        public string ZusatzinformationInhalt8 { get; set; }

        [DatevPosition(63)]
        public string ZusatzinformationArt9 { get; set; }

        [DatevPosition(64)]
        public string ZusatzinformationInhalt9 { get; set; }

        [DatevPosition(65)]
        public string ZusatzinformationArt10 { get; set; }

        [DatevPosition(66)]
        public string ZusatzinformationInhalt10 { get; set; }

        [DatevPosition(67)]
        public string ZusatzinformationArt11 { get; set; }

        [DatevPosition(68)]
        public string ZusatzinformationInhalt11 { get; set; }

        [DatevPosition(69)]
        public string ZusatzinformationArt12 { get; set; }

        [DatevPosition(70)]
        public string ZusatzinformationInhalt12 { get; set; }

        [DatevPosition(71)]
        public string ZusatzinformationArt13 { get; set; }

        [DatevPosition(72)]
        public string ZusatzinformationInhalt13 { get; set; }

        [DatevPosition(73)]
        public string ZusatzinformationArt14 { get; set; }

        [DatevPosition(74)]
        public string ZusatzinformationInhalt14 { get; set; }

        [DatevPosition(75)]
        public string ZusatzinformationArt15 { get; set; }

        [DatevPosition(76)]
        public string ZusatzinformationInhalt15 { get; set; }

        [DatevPosition(77)]
        public string ZusatzinformationArt16 { get; set; }

        [DatevPosition(78)]
        public string ZusatzinformationInhalt16 { get; set; }

        [DatevPosition(79)]
        public string ZusatzinformationArt17 { get; set; }

        [DatevPosition(80)]
        public string ZusatzinformationInhalt17 { get; set; }

        [DatevPosition(81)]
        public string ZusatzinformationArt18 { get; set; }

        [DatevPosition(82)]
        public string ZusatzinformationInhalt18 { get; set; }

        [DatevPosition(83)]
        public string ZusatzinformationArt19 { get; set; }

        [DatevPosition(84)]
        public string ZusatzinformationInhalt19 { get; set; }

        [DatevPosition(85)]
        public string ZusatzinformationArt20 { get; set; }

        [DatevPosition(86)]
        public string ZusatzinformationInhalt20 { get; set; }

        [DatevPosition(87)]
        public decimal? Stueck { get; set; }

        [DatevPosition(88)]
        public decimal? Gewicht { get; set; }

        [DatevPosition(89)]
        public int? Zahlweise { get; set; }

        [DatevPosition(90)]
        public string Forderungsart { get; set; }

        [DatevPosition(91)]
        public int? Veranlagungsjahr { get; set; }

        [DatevPosition(92)]
        [DatevFormat("ddMMyyyy")]
        public DateTime? ZugeordneteFaelligkeit { get; set; }

        [DatevPosition(93)]
        public int? Skontotyp { get; set; }

        [DatevPosition(94)]
        public string Auftragsnummer { get; set; }

        [DatevPosition(95)]
        public string Buchungstyp { get; set; }

        [DatevPosition(96)]
        public int? UStSchluesselAnzahlungen { get; set; }

        [DatevPosition(97)]
        public string EUAnzahlungenLand { get; set; }

        [DatevPosition(98)]
        public int? LLAnzahlungenSachverhalt { get; set; }

        [DatevPosition(99)]
        public decimal? EUAnzahlungenSteuersatz { get; set; }

        [DatevPosition(100)]
        public string ErloeskontoAnzahlungen { get; set; }

        [DatevPosition(101)]
        public string HerkunftKz { get; set; }

        [DatevPosition(102)]
        public string Leerfeld1 { get; set; }

        [DatevPosition(103)]
        [DatevFormat("ddMMyyyy")]
        public DateTime? KostDatum { get; set; }

        [DatevPosition(104)]
        public string SepaMandatsreferenz { get; set; }

        [DatevPosition(105)]
        public int? Skontosperre { get; set; }

        [DatevPosition(106)]
        public string Gesellschaftername { get; set; }

        [DatevPosition(107)]
        public int? Beteiligtennummer { get; set; }

        [DatevPosition(108)]
        public string Identifikationsnummer { get; set; }

        [DatevPosition(109)]
        public string Zeichnernummer { get; set; }

        [DatevPosition(110)]
        [DatevFormat("ddMMyyyy")]
        public DateTime? PostensperreBis { get; set; }

        [DatevPosition(111)]
        public string BezeichnungSoBilSachverhalt { get; set; }

        [DatevPosition(112)]
        public int? KennzeichenSoBilBuchung { get; set; }

        [DatevPosition(113)]
        public int? Festschreibung { get; set; }

        [DatevPosition(114)]
        [DatevFormat("ddMMyyyy")]
        public DateTime? Leistungsdatum { get; set; }

        [DatevPosition(115)]
        [DatevFormat("ddMMyyyy")]
        public DateTime? DatumZuordnungSteuerperiode { get; set; }

        [DatevPosition(116)]
        [DatevFormat("ddMMyyyy")]
        public DateTime? Faelligkeit { get; set; }

        [DatevPosition(117)]
        public string Generalumkehr { get; set; }

        [DatevPosition(118)]
        public decimal? Steuersatz { get; set; }

        [DatevPosition(119)]
        public string Land { get; set; }

        [DatevPosition(120)]
        public string Abrechnungsreferenz { get; set; }

        [DatevPosition(121)]
        public int? BvvPosition { get; set; }

        [DatevPosition(122)]
        public string EUUrsprungLandUStId { get; set; }

        [DatevPosition(123)]
        public decimal? EUUrsprungSteuersatz { get; set; }

        [DatevPosition(124)]
        public string AbwSkontokonto { get; set; }
    }

}
