using CreateIf.Datev.Models;
using DatevSharp.CSV.Helper;
using System;
using System.IO;
using System.Text;

namespace DatevSharp.Tests
{
    public class DatevHeaderTests
    {
        private Encoding _encoding => Encoding.GetEncoding("ISO-8859-1");
        private const string TestFilePath = "Data/EXTF_Header.csv";

        [Fact]
        public void Should_Import_Header()
        {
            using var input = File.OpenRead(TestFilePath);

            using var reader = new StreamReader(input, _encoding, true);
            using var csv = new DatevCsvReader(reader);

            var header = csv.GetHeader();
            Assert.Equal(header, csv.Header);
            Assert.Equal("EXTF", header.Kennzeichen);
            Assert.Equal(700, header.Versionsnummer);
            Assert.Equal(21, header.Formatkategorie);
            Assert.Equal("Buchungsstapel", header.Formatname);
            Assert.Equal(13, header.Formatversion);
            Assert.Equal(new DateTime(2024, 1, 30, 14, 04, 40, 439), header.ErzeugtAm); // aus 20240130140440439
            Assert.Null(header.Importiert);
            Assert.Equal("RE", header.Herkunft);
            Assert.Equal(string.Empty, header.ExportiertVon);
            Assert.Equal(string.Empty, header.ImportiertVon);
            Assert.Equal(29098, header.Beraternummer);
            Assert.Equal(55003, header.Mandantennummer);
            Assert.Equal(new DateTime(2024, 1, 1), header.Wirtschaftsjahresbeginn);
            Assert.Equal(4, header.Sachkontenlaenge);
            Assert.Equal(new DateTime(2024, 1, 1), header.DatumVon);
            Assert.Equal(new DateTime(2024, 8, 31), header.DatumBis);
            Assert.Equal("Buchungsstapel", header.Bezeichnung);
            Assert.Equal("WD", header.Diktatkürzel);
            Assert.Equal(1, header.Buchungstyp);
            Assert.Equal(0, header.Rechnungslegungszweck);
            Assert.Equal(0, header.Festschreibung);
            Assert.Equal("EUR", header.Waehrung);
            Assert.Equal("03", header.Sachkontenrahmen);
            Assert.Null(header.Reserviert1);
            Assert.Equal(string.Empty, header.Derivatskennzeichen);
            Assert.Null(header.Reserviert2);
            Assert.Null(header.Reserviert3);
            Assert.Null(header.BranchenloesungId);
            Assert.Null(header.Reserviert4);
            Assert.Equal(string.Empty, header.Reserviert5);
            Assert.Equal(string.Empty, header.Anwendungsinformation);
        }

        [Fact]
        public void Should_Export_Header()
        {
            // Arrange: Header so füllen, dass alle Spezialfälle (null, string.Empty) vorkommen
            var header = new DatevHeader
            {
                Kennzeichen = "EXTF",
                Versionsnummer = 700,
                Formatkategorie = 21,
                Formatname = "Buchungsstapel",
                Formatversion = 13,
                ErzeugtAm = new DateTime(2024, 1, 30, 14, 04, 40, 439),
                Importiert = null,                 // wird zu ;;
                Herkunft = "RE",
                ExportiertVon = string.Empty,      // wird zu ""
                ImportiertVon = string.Empty,      // wird zu ""
                Beraternummer = 29098,
                Mandantennummer = 55003,
                Wirtschaftsjahresbeginn = new DateTime(2024, 1, 1),
                Sachkontenlaenge = 4,
                DatumVon = new DateTime(2024, 1, 1),
                DatumBis = new DateTime(2024, 8, 31),
                Bezeichnung = "Buchungsstapel",
                Diktatkürzel = "WD",
                Buchungstyp = 1,
                Rechnungslegungszweck = 0,
                Festschreibung = 0,
                Waehrung = "EUR",
                Reserviert1 = null,
                Derivatskennzeichen = string.Empty,
                Reserviert2 = null,
                Reserviert3 = null,
                Sachkontenrahmen = "03",
                BranchenloesungId = null,
                Reserviert4 = null,
                Reserviert5 = string.Empty,
                Anwendungsinformation = string.Empty
            };

            using var sw = new StringWriter();
            using (var writer = new DatevCsvWriter(sw, ";"))
            {
                writer.WriteRecord(header);
            }

            var result = sw.ToString().Trim();

            // Erwartete exakte CSV-Zeile gemäß DATEV-Doku:
            var expected = "\"EXTF\";700;21;\"Buchungsstapel\";13;20240130140440439;;\"RE\";\"\";\"\";29098;55003;20240101;4;20240101;20240831;\"Buchungsstapel\";\"WD\";1;0;0;\"EUR\";;\"\";;;" +
                           "\"03\";;;\"\";\"\"";

            Assert.Equal(expected, result);

            expected = File.ReadAllText(TestFilePath).Trim();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Should_Export_Buchungsstapel_Header()
        {
            string path = "Data/EXTF_Buchungsstapel.csv";
            var lines = File.ReadAllLines(path, _encoding);
            var headerLine = lines[1];

            Assert.Equal(headerLine, DatevCsvHeaders.BookingBatch);
        }

        [Fact]
        public void Should_Export_WiederkehrendeBuchungen_Header()
        {
            string path = "Data/EXTF_Wiederkehrende-Buchungen.csv";
            var lines = File.ReadAllLines(path, _encoding);
            var headerLine = lines[1];
            Assert.Equal(headerLine, DatevCsvHeaders.RecurringBookings);
        }

        [Fact]
        public void Should_Export_Debitoren_Header()
        {
            string path = "Data/EXTF_DebKred_Stamm.csv";
            var lines = File.ReadAllLines(path, _encoding);
            var headerLine = lines[1];
            Assert.Equal(headerLine, DatevCsvHeaders.DebitorsCreditors);
        }

        [Fact]
        public void Should_Export_Sachkonten_Header()
        {
            string path = "Data/EXTF_Sachkontobeschriftungen.csv";
            var lines = File.ReadAllLines(path, _encoding);
            var headerLine = lines[1];
            Assert.Equal(headerLine, DatevCsvHeaders.GlAccountDescription);
        }

        [Fact]
        public void Should_Export_DiverseAdressen_Header()
        {
            string path = "Data/EXTF_Div-Adressen.csv";
            var lines = File.ReadAllLines(path, _encoding);
            var headerLine = lines[1];
            Assert.Equal(headerLine, DatevCsvHeaders.VariousAddresses);
        }

        [Fact]
        public void Should_Export_Naturalstapel_Header()
        {
            string path = "Data/EXTF_Naturalstapel.csv";
            var lines = File.ReadAllLines(path, _encoding);
            var headerLine = lines[1];
            Assert.Equal(headerLine, DatevCsvHeaders.NaturalStack);
        }
    }
}