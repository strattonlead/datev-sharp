using CreateIf.Datev.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Globalization;

namespace DatevSharp.EXIF.Models
{
    public sealed class DatevHeaderCsvMap : ClassMap<DatevHeader>
    {
        public DatevHeaderCsvMap()
        {
            Map(m => m.Kennzeichen).Index(0);
            Map(m => m.Versionsnummer).Index(1);
            Map(m => m.Formatkategorie).Index(2);
            Map(m => m.Formatname).Index(3);
            Map(m => m.Formatversion).Index(4);
            Map(m => m.ErzeugtAm).Index(5)
                .TypeConverter(new DateTimeCustomConverter("yyyyMMddHHmmssfff"));
            Map(m => m.Importiert).Index(6);
            Map(m => m.Herkunft).Index(7);
            Map(m => m.ExportiertVon).Index(8);
            Map(m => m.ImportiertVon).Index(9);
            Map(m => m.Beraternummer).Index(10);
            Map(m => m.Mandantennummer).Index(11);
            Map(m => m.Wirtschaftsjahresbeginn).Index(12);
            Map(m => m.Sachkontenlaenge).Index(13);
            Map(m => m.DatumVon).Index(14);
            Map(m => m.DatumBis).Index(15);
            Map(m => m.Bezeichnung).Index(16);
            Map(m => m.Diktatkürzel).Index(17);
            Map(m => m.Buchungstyp).Index(18);
            Map(m => m.Rechnungslegungszweck).Index(19);
            Map(m => m.Festschreibung).Index(20);
            Map(m => m.Waehrung).Index(21);
            Map(m => m.Reserviert22).Index(22);
            Map(m => m.Derivatskennzeichen).Index(23);
            Map(m => m.Reserviert24).Index(24);
            Map(m => m.Reserviert25).Index(25);
            Map(m => m.Reserviert26).Index(26);
            Map(m => m.Sachkontenrahmen).Index(27);
            Map(m => m.BranchenloesungId).Index(28);
            Map(m => m.Reserviert29).Index(29);
            Map(m => m.Anwendungsinformation).Index(30);
        }
    }

    public class DateTimeCustomConverter : DefaultTypeConverter
    {
        private readonly string _format;

        public DateTimeCustomConverter(string format)
        {
            _format = format;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is DateTime dt)
                return dt.ToString(_format, CultureInfo.InvariantCulture);

            return string.Empty;
        }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (DateTime.TryParseExact(text, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;

            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
