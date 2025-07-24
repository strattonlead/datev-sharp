using CreateIf.Datev.Models;
using CsvHelper;
using CsvHelper.Configuration;
using DatevSharp.EXIF.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DatevSharp.EXIF.Services
{
    public class CsvExportService
    {
        private readonly CsvConfiguration _config;

        public CsvExportService()
        {
            //_config = new CsvConfiguration(new CultureInfo("de-DE"))
            _config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Encoding = Encoding.GetEncoding("ISO-8859-1"),
                NewLine = "\r\n",
                ShouldQuote = _shouldQuote
            };
        }

        private bool _shouldQuote(ShouldQuoteArgs args)
             => args.FieldType == typeof(string);

        public void ExportBuchungsstapel(Stream stream, Formatkategorie kategorie, int beraternummer, int mandantennummer, IEnumerable<Buchung> daten, string bezeichnung = null)
        {
            var header = DatevHeaderBuilder.Create(kategorie, beraternummer, mandantennummer, bezeichnung);
            ExportBuchungsstapel(stream, header, daten);
        }

        public void ExportBuchungsstapel(Stream outputStream, DatevHeader header, IEnumerable<Buchung> buchungen)
        {
            using var writer = new StreamWriter(outputStream, _config.Encoding, 1024, true);
            using var csv = new CsvWriter(writer, _config);
            csv.Context.RegisterClassMap<DatevHeaderCsvMap>();

            // 1. Header schreiben (erste Zeile)
            csv.WriteRecord(header);
            //var headerProps = typeof(DatevHeader).GetProperties();
            //foreach (var prop in headerProps)
            //{
            //    csv.WriteField(prop.GetValue(header));
            //}
            csv.NextRecord();

            // 2. Überschriftenzeile (Spaltennamen – optional, aber nützlich)
            var properties = typeof(Buchung).GetProperties();
            foreach (var prop in properties)
            {
                csv.WriteField(prop.Name);
            }
            csv.NextRecord();

            // 3. Buchungsdaten
            csv.WriteRecords(buchungen);

            writer.Flush();
        }

        public void ExportDebitoren(Stream outputStream, DatevHeader header, IEnumerable<DebitorKreditor> debitoren)
        {
            using var writer = new StreamWriter(outputStream, _config.Encoding, 1024, true);
            using var csv = new CsvWriter(writer, _config);

            // 1. Header schreiben
            var headerValues = GetHeaderValues(header);
            csv.WriteField(headerValues);
            csv.NextRecord();

            // 2. Überschriftenzeile schreiben (optional)
            var properties = typeof(DebitorKreditor).GetProperties();
            foreach (var prop in properties)
            {
                csv.WriteField(prop.Name);
            }
            csv.NextRecord();

            // 3. Datensätze schreiben
            foreach (var debitor in debitoren)
            {
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(debitor);
                    csv.WriteField(value);
                }
                csv.NextRecord();
            }

            writer.Flush();
        }


        private string GetHeaderValues(DatevHeader header)
        {
            var properties = typeof(DatevHeader).GetProperties();
            var values = properties.Select(p => p.GetValue(header)?.ToString() ?? "");
            return string.Join(";", values);
        }
    }
}
