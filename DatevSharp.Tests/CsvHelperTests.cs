using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DatevSharp.Tests
{
    public class CsvHelperTests
    {
        [Fact]
        public void WriteAndRead_DateTime_WithCustomFormat_Works()
        {
            // Arrange
            var records = new[]
                {
                new SampleRecord {
                    Text = "String Values",
                    Timestamp = new DateTime(2025, 07, 23, 14, 35, 59, 123) }
            };

            //_config = new CsvConfiguration(new CultureInfo("de-DE"))
            var _config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Encoding = Encoding.GetEncoding("ISO-8859-1"),
                NewLine = "\r\n",
                ShouldQuote = args => args.FieldType == typeof(string)
            };
            string csvOutput;
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, _config))
            {
                csv.Context.RegisterClassMap<SampleRecordMap>();
                csv.WriteRecords(records);
                csvOutput = writer.ToString();
            }

            // Assert: CSV enthält korrekt formatiertes Datum
            Assert.Contains("20250723143559123", csvOutput);
            Assert.DoesNotContain("\"20250723143559123\"", csvOutput);

            Assert.Contains("\"String Values\"", csvOutput);

            // Act: Jetzt wieder einlesen
            SampleRecord readRecord;
            using (var reader = new StringReader(csvOutput))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<SampleRecordMap>();
                readRecord = csv.GetRecords<SampleRecord>().First();
            }

            // Assert: Datum wurde korrekt geparst
            Assert.Equal(records[0].Timestamp, readRecord.Timestamp);
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

    public class SampleRecord
    {
        [Index(0)]
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public sealed class SampleRecordMap : ClassMap<SampleRecord>
    {
        public SampleRecordMap()
        {
            Map(m => m.Text)
               .Index(0);

            Map(m => m.Timestamp)
                .Index(1)
                .TypeConverter(new DateTimeCustomConverter("yyyyMMddHHmmssfff"));
        }
    }
}
