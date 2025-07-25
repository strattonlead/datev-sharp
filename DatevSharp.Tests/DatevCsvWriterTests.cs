using DatevSharp.CSV.Helper;
using System;
using System.IO;

namespace DatevSharp.Tests
{
    public class DatevCsvWriterTests
    {
        private class TestRecord
        {
            [DatevPosition(0)]
            public string Feld1 { get; set; }

            [DatevPosition(1)]
            public string Feld2 { get; set; }

            [DatevPosition(2)]
            [DatevFormat("ddMMyyyy")] // TTMMJJJJ
            public DateTime? Datum1 { get; set; }

            [DatevPosition(3)]
            [DatevFormat("yyyyMMdd")] // JJJJMMTT
            public DateTime? Datum2 { get; set; }
        }

        [Fact]
        public void WriteRecord_Should_Distinguish_Null_Empty_And_Format_Dates_Unquoted()
        {
            // Arrange
            var record = new TestRecord
            {
                Feld1 = null,
                Feld2 = string.Empty,
                Datum1 = new DateTime(2025, 7, 24),
                Datum2 = new DateTime(2024, 1, 1)
            };

            using var sw = new StringWriter();
            var writer = new DatevCsvWriter(sw, ";");

            // Act
            writer.WriteRecord(record);
            writer.Dispose();

            var result = sw.ToString().Trim();

            // Assert
            // Erwartet:
            // Feld1: null -> ; 
            // Feld2: string.Empty -> "" 
            // Datum1: 24072025 unquoted
            // Datum2: 20240101 unquoted
            Assert.Equal(";\"\";24072025;20240101", result);
        }
    }
}
