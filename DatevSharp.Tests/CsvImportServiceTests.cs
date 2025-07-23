using CreateIf.Datev.Services;
using System.IO;
using System.Text;

namespace DatevSharp.Tests
{
    public class CsvImportServiceTests
    {
        [Fact]
        public void Should_Import_Header_And_Buchung()
        {
            var csv =
                "\"EXTF\";700;21;\"Buchungsstapel\";13;20240130140440439;;\"RE\";\"CreateIf\";\"\";29098;55003;20240101;4;20240101;20240831;\"Buchungsstapel\";\"WD\";1;0;0;\"EUR\"\r\n" +
                "Umsatz;SollHabenKennzeichen;WKZ Umsatz;Konto;Gegenkonto;BU-Schlüssel;Buchungstext\r\n" +
                "100,99;\"S\";\"EUR\";8400;1200;\"1000\";\"Test\"\r\n";

            using var stream = new MemoryStream(Encoding.GetEncoding("ISO-8859-1").GetBytes(csv));

            var service = new CsvImportService();
            var (header, buchungen) = service.ImportBuchungsstapel(stream);

            Assert.Equal("EXTF", header.Kennzeichen);
            Assert.Single(buchungen);
            Assert.Equal(100.99m, buchungen[0].Umsatz);
            Assert.Equal("8400", buchungen[0].Konto);
            Assert.Equal("1200", buchungen[0].Gegenkonto);
        }

        [Fact]
        public void Should_Import_Debitoren()
        {
            var csv =
                "\"EXTF\";700;16;\"Debitoren/Kreditoren\";5;20240130140659583;;\"RE\";\"\";\"\";29098;55003;20240101;4;;;;\"\";\"\";;;;\"\";;\"\";;\"03\";;\"\";\"\"\r\n" +
                "Konto;NameUnternehmen;...;Kurzbezeichnung;...;Ort;Land;...;Email;...;IBAN1;SWIFT1;...\r\n" +
                "10001;\"Muster GmbH\";...;\"MUSTER\";...;\"Musterstadt\";\"DE\";...;\"info@muster.de\";...;\"DE12345678901234567890\";\"DEUTDEFF\";...\r\n";

            using var stream = new MemoryStream(Encoding.GetEncoding("ISO-8859-1").GetBytes(csv));
            var service = new CsvImportService();

            var debitoren = service.ImportDebitoren(stream);

            Assert.Single(debitoren);
            var d = debitoren[0];
            Assert.Equal("10001", d.Konto);
            Assert.Equal("\"MUSTER\"", d.Kurzbezeichnung);
            Assert.Equal("\"Musterstadt\"", d.Ort);
            Assert.Equal("\"DE\"", d.Land);
        }

    }
}
