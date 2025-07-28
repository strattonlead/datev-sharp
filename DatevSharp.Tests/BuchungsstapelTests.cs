using DatevSharp.CSV.Helper;
using DatevSharp.EXIF.Models;
using System.IO;
using System.Text;

namespace DatevSharp.Tests
{
    public class BuchungsstapelTests
    {
        private readonly Encoding _encoding = Encoding.GetEncoding("ISO-8859-1");

        [Fact]
        public void Should_Export_Single_Buchungsstapel_Record()
        {
            // Arrange: Beispiel-Datensatz
            var record = new Buchungsstapel
            {
                Umsatz = 100.18m,
                SollHabenKennzeichen = "S",
                WkzUmsatz = "",
                WkzBasisUmsatz = "",
                Konto = 48400,
                Gegenkonto = 8401,
                BUSchluessel = "",
                Belegdatum = new System.DateTime(2025, 1, 31),
                Belegfeld1 = "",
                Belegfeld2 = "",
                Buchungstext = "Test Anzahlung",
                DiverseAdressnummer = "",
                Geschaeftspartnerbank = 1,
                Veranlagungsjahr = 2012,
                Skontotyp = 1,
                Auftragsnummer = "Projekt 4711",
                Buchungstyp = "AG",
                UStSchluesselAnzahlungen = 3,
                ErloeskontoAnzahlungen = "8070",
                HerkunftKz = "WK",
                KOST1 = "50",
                Skontosperre = 0
            };

            // Erwartete CSV-Zeile (wie aus deinem Beispiel, ohne Header)
            var expected =
                "100,18;\"S\";\"\";;;\"\";48400;8401;\"\";3101;\"\";\"\";;\"Test Anzahlung\";;\"\";1;;\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"50\";\"\";;\"\";;\"\";;\"\";;;;\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";;;;\"\";2012;;1;\"Projekt 4711\";\"AG\";3;\"\";;;8070;\"WK\";\"\";;\"\";;\"\";;\"\";\"\";;\"\";;0;;;;\"\";;\"\";\"\";;\"\";;";

            // Act: Exportieren
            using var sw = new StringWriter();
            using (var csvWriter = new DatevCsvWriter(sw, ";"))
            {
                csvWriter.WriteRecord(record);
            }
            var result = sw.ToString().Trim();

            // Assert
            Assert.Equal(expected, result);
        }

    }
}
