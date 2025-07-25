using CreateIf.Datev.Models;
using DatevSharp.CSV.Helper;
using DatevSharp.EXIF.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DatevSharp.EXIF.Services
{
    public class CsvExportService
    {
        private Encoding _encoding => Encoding.GetEncoding("ISO-8859-1");

        public void ExportBuchungsstapel(Stream stream, Formatkategorie kategorie, int beraternummer, int mandantennummer, IEnumerable<Buchung> daten, string bezeichnung = null)
        {
            var header = DatevHeaderBuilder.Create(kategorie, beraternummer, mandantennummer, bezeichnung);
            ExportBuchungsstapel(stream, header, daten);
        }

        public void ExportBuchungsstapel(Stream outputStream, DatevHeader header, IEnumerable<Buchung> buchungen)
        {
            using var writer = new StreamWriter(outputStream, _encoding, 1024, true);
            using var csv = new DatevCsvWriter(writer);

            // 1. Header schreiben (erste Zeile)
            csv.WriteRecord(header);

            // 2. Überschriftenzeile (Spaltennamen – optional, aber nützlich)
            //var properties = typeof(Buchung).GetProperties();
            //foreach (var prop in properties)
            //{
            //    csv.WriteField(prop.Name);
            //}
            //csv.NextRecord();

            // 3. Buchungsdaten
            foreach (var buchung in buchungen)
            {
                csv.WriteRecord(buchung);
            }

            writer.Flush();
        }

        public void ExportDebitoren(Stream outputStream, DatevHeader header, IEnumerable<DebitorKreditor> debitoren)
        {
            using var writer = new StreamWriter(outputStream, _encoding, 1024, true);
            using var csv = new DatevCsvWriter(writer);

            // 1. Header schreiben
            csv.WriteRecord(header);

            // 2. Überschriftenzeile schreiben (optional)
            //var properties = typeof(DebitorKreditor).GetProperties();
            //foreach (var prop in properties)
            //{
            //    csv.WriteField(prop.Name);
            //}
            //csv.NextRecord();

            // 3. Datensätze schreiben
            foreach (var debitor in debitoren)
            {
                csv.WriteRecord(debitor);
            }

            writer.Flush();
        }
    }
}
