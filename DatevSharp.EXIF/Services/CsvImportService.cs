using CreateIf.Datev.Models;
using DatevSharp.CSV.Helper;
using DatevSharp.EXIF.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CreateIf.Datev.Services
{
    public class CsvImportService
    {
        private Encoding _encoding => Encoding.GetEncoding("ISO-8859-1");

        /// <summary>
        /// Liest einen DATEV-Buchungsstapel (Header + Buchungen).
        /// </summary>
        public (DatevHeader Header, List<Buchungsstapel> Buchungen) ImportBuchungsstapel(Stream input)
        {
            using var reader = new StreamReader(input, _encoding, true);
            using var csv = new DatevCsvReader(reader);

            // Header lesen (erste Zeile) & cachen
            var header = csv.GetHeader();
            if (header == null)
                throw new InvalidDataException("Die CSV-Datei enthält keine Headerzeile.");

            // Spaltenüberschriften (zweite Zeile) überspringen
            if (csv.ReadLine() == null)
                throw new InvalidDataException("Die CSV-Datei enthält keine Überschriftszeile.");

            // Alle weiteren Zeilen als Buchung einlesen
            var buchungen = csv.ReadRecords<Buchungsstapel>().ToList();

            return (header, buchungen);
        }

        /// <summary>
        /// Liest Debitoren/Kreditoren-Stammdaten (Header + Records).
        /// </summary>
        public List<DebitorKreditor> ImportDebitoren(Stream input)
        {
            using var reader = new StreamReader(input, _encoding, true);
            using var csv = new DatevCsvReader(reader);

            // Header (erste Zeile) überspringen & cachen
            var header = csv.GetHeader();
            if (header == null)
                throw new InvalidDataException("DATEV Header fehlt.");

            // Spaltenüberschriften (zweite Zeile) überspringen
            if (csv.ReadLine() == null)
                throw new InvalidDataException("Überschrift fehlt.");

            // Alle weiteren Zeilen als Debitor/Kreditor einlesen
            return csv.ReadRecords<DebitorKreditor>().ToList();
        }
    }
}
