//using CreateIf.Datev.Services;
//using DatevSharp.EXIF.Services;
//using System.IO;
//using System.Text;

//namespace DatevSharp.Tests
//{
//    public class DebitorenRoundtripTests
//    {
//        private const string TestFilePath = "Data/EXTF_Debitoren.csv";

//        [Fact]
//        public void Should_Import_And_Export_Debitoren_Without_Changes()
//        {
//            // Arrange
//            var importService = new CsvImportService();
//            var exportService = new CsvExportService();

//            // 1. Original-Datei lesen
//            using var input = File.OpenRead(TestFilePath);
//            var  datensaetze = importService.ImportDebitoren(input); // ggf. korrigieren auf ImportDebitoren
//            input.Close();

//            // 2. In Memory exportieren
//            using var memory = new MemoryStream();
//            exportService.ExportDebitoren(memory, header, datensaetze); // ggf. korrigieren auf List<DebitorKreditor>

//            // 3. Bytevergleich nach Zeichensatz
//            var original = File.ReadAllText(TestFilePath, Encoding.GetEncoding("ISO-8859-1"));
//            var generated = Encoding.GetEncoding("ISO-8859-1").GetString(memory.ToArray());

//            Assert.Equal(Normalize(original), Normalize(generated));
//        }

//        private static string Normalize(string s)
//        {
//            return s.Replace("\r\n", "\n").Trim();
//        }
//    }
//}
