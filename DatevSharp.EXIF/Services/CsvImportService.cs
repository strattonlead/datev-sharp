using CreateIf.Datev.Models;
using CsvHelper;
using CsvHelper.Configuration;
using DatevSharp.EXIF.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace CreateIf.Datev.Services
{
    public class CsvImportService
    {
        private readonly CsvConfiguration _config;

        public CsvImportService()
        {
            _config = new CsvConfiguration(new CultureInfo("de-DE"))
            {
                Delimiter = ";",
                Encoding = Encoding.GetEncoding("ISO-8859-1"),
                HasHeaderRecord = false,
                NewLine = "\r\n",
                BadDataFound = null,
                MissingFieldFound = null
            };
        }

        public (DatevHeader Header, List<Buchung> Buchungen) ImportBuchungsstapel(Stream input)
        {
            using var reader = new StreamReader(input, _config.Encoding, true);
            using var csv = new CsvReader(reader, _config);

            if (!csv.Read())
                throw new InvalidDataException("Die CSV-Datei enthält keine Headerzeile.");

            var headerValues = csv.Context.Parser.Record;
            if (headerValues is null || headerValues.Length < 22)
                throw new InvalidDataException("Ungültiger DATEV-Header oder Datei leer.");

            var header = ParseHeader(headerValues);

            if (!csv.Read())
                throw new InvalidDataException("Die CSV-Datei enthält keine Überschriftszeile.");

            // Spaltenüberschrift (zweite Zeile) ignorieren
            var buchungen = new List<Buchung>();

            while (csv.Read())
            {
                var buchung = csv.GetRecord<Buchung>();
                buchungen.Add(buchung);
            }

            return (header, buchungen);
        }

        public List<DebitorKreditor> ImportDebitoren(Stream input)
        {
            using var reader = new StreamReader(input, _config.Encoding, true);
            using var csv = new CsvReader(reader, _config);

            var result = new List<DebitorKreditor>();

            // Header (EXTF...) überspringen
            if (!csv.Read()) throw new InvalidDataException("DATEV Header fehlt.");
            // Spaltenüberschriften überspringen
            if (!csv.Read()) throw new InvalidDataException("Überschrift fehlt.");

            while (csv.Read())
            {
                var record = csv.GetRecord<DebitorKreditor>();
                result.Add(record);
            }

            return result;
        }



        private DatevHeader ParseHeader(string[] values)
        {
            if (values.Length < 22)
                throw new InvalidDataException("Ungültiger DATEV-Header: zu wenige Felder.");

            return new DatevHeader
            {
                Kennzeichen = values[0],
                Versionsnummer = int.Parse(values[1]),
                Formatkategorie = int.Parse(values[2]),
                Formatname = values[3],
                Formatversion = int.Parse(values[4]),
                ErzeugtAm = DateTime.ParseExact(values[5], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture),
                Herkunft = values[7],
                ExportiertVon = values[8],
                ImportiertVon = values[9],
                Beraternummer = int.Parse(values[10]),
                Mandantennummer = int.Parse(values[11]),
                Wirtschaftsjahresbeginn = values[12],
                Sachkontenlaenge = int.Parse(values[13]),
                DatumVon = values[14],
                DatumBis = values[15],
                Bezeichnung = values[16],
                Diktatkürzel = values[17],
                Buchungstyp = int.Parse(values[18]),
                Rechnungslegungszweck = int.Parse(values[19]),
                Festschreibung = int.Parse(values[20]),
                Waehrung = values[21]
            };
        }
    }
}
