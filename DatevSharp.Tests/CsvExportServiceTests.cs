using CreateIf.Datev.Models;
using DatevSharp.EXIF.Models;
using DatevSharp.EXIF.Services;
using System.IO;
using System.Text;

namespace DatevSharp.Tests
{
    public class CsvExportServiceTests
    {
        [Fact]
        public void Should_Export_Csv_With_Header_And_One_Buchung()
        {
            var header = new DatevHeader
            {
                Formatkategorie = 21,
                Formatversion = 13,
                Beraternummer = 1234,
                Mandantennummer = 56789,
                Wirtschaftsjahresbeginn = new System.DateTime(2025, 1, 1),
                Sachkontenlaenge = 4
            };

            var buchung = new Buchung
            {
                Umsatz = 100.99m,
                Konto = "8400",
                Gegenkonto = "1200",
                BUSchluessel = "\"1000\"",
                Buchungstext = "\"Testbuchung\""
            };

            using var stream = new MemoryStream();
            var service = new CsvExportService();
            service.ExportBuchungsstapel(stream, header, new[] { buchung });

            stream.Position = 0;
            var output = new StreamReader(stream, Encoding.GetEncoding("ISO-8859-1")).ReadToEnd();

            Assert.Contains("EXTF", output);
            Assert.Contains("Buchungsstapel", output);
            Assert.Contains("100,99", output); // Betrag
            Assert.Contains("8400", output);   // Konto
            Assert.Contains("1200", output);   // Gegenkonto
            Assert.Contains("\"1000\"", output); // BU-Schlüssel
        }

        [Fact]
        public void Should_Export_Debitoren_As_Csv()
        {
            var header = new DatevHeader
            {
                Formatkategorie = 16,
                Formatversion = 5,
                Beraternummer = 1234,
                Mandantennummer = 56789,
                Wirtschaftsjahresbeginn = new System.DateTime(2025, 1, 1),
                Sachkontenlaenge = 4,
                Formatname = "\"Debitoren/Kreditoren\"",
                Bezeichnung = "\"Debitoren\""
            };

            var debitor = new DebitorKreditor
            {
                Konto = "10001",
                Kurzbezeichnung = "\"TEST\"",
                Ort = "\"Teststadt\"",
                Land = "\"DE\"",
                IBAN1 = "\"DE12345678901234567890\"",
                SWIFT1 = "\"DEUTDEFF\""
            };

            using var stream = new MemoryStream();
            var service = new CsvExportService();
            service.ExportDebitoren(stream, header, new[] { debitor });

            stream.Position = 0;
            var result = new StreamReader(stream, Encoding.GetEncoding("ISO-8859-1")).ReadToEnd();

            Assert.Contains("\"EXTF\"", result);
            Assert.Contains("10001", result);
            Assert.Contains("DE12345678901234567890", result);
            Assert.Contains("DEUTDEFF", result);
        }
    }
}
