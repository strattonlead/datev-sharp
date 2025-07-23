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
            Map(m => m.ErzeugtAm)
                .Index(5)
                .TypeConverter(new DateTimeCustomConverter("yyyyMMddHHmmssfff"));
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
