using CreateIf.Datev.Services;
using DatevSharp.EXIF.Services;
using System.IO;
using System.Text;

namespace DatevSharp.Tests
{
    public class BuchungsstapelRoundtripTests
    {
        private const string TestFilePath = "Data/EXTF_Buchungsstapel.csv";

        [Fact]
        public void Should_Import_And_Export_Buchungsstapel_Without_Changes()
        {
            var importService = new CsvImportService();
            var exportService = new CsvExportService();

            // 1. Original einlesen
            using var input = File.OpenRead(TestFilePath);
            var (header, buchungen) = importService.ImportBuchungsstapel(input);

            // 2. In Memory wieder exportieren
            using var memory = new MemoryStream();
            exportService.ExportBuchungsstapel(memory, header, buchungen);

            // 3. Inhalt vergleichen
            var original = File.ReadAllText(TestFilePath, Encoding.GetEncoding("ISO-8859-1"));
            var generated = Encoding.GetEncoding("ISO-8859-1").GetString(memory.ToArray());

            AssertEachLine(original, generated);
            Assert.Equal(NormalizeLineEndings(original), NormalizeLineEndings(generated));
        }

        private static void AssertEachLine(string original, string generated)
        {
            var originalLines = NormalizeLineEndings(original).Split('\n');
            var generatedLines = NormalizeLineEndings(generated).Split('\n');
            Assert.Equal(originalLines.Length, generatedLines.Length);
            for (int i = 0; i < originalLines.Length; i++)
            {
                Assert.Equal(originalLines[i], generatedLines[i]);
            }
        }


        private static string NormalizeLineEndings(string input) =>
            input.Replace("\r\n", "\n").Replace("\r", "\n").Trim();
    }
}
