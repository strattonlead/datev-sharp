using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DatevSharp.CSV.Helper
{
    public class DatevCsvWriter : IDisposable
    {
        private readonly TextWriter _writer;
        private readonly string _delimiter;

        public DatevCsvWriter(TextWriter writer, string delimiter = ";")
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _delimiter = delimiter;
        }

        public void WriteRecord(object record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));

            var props = record.GetType().GetProperties()
                .Select(p => new
                {
                    Property = p,
                    Position = p.GetCustomAttributes(typeof(DatevPositionAttribute), false)
                                .Cast<DatevPositionAttribute>()
                                .FirstOrDefault()?.Position ?? int.MaxValue, // Ohne Attribut ans Ende
                    Format = p.GetCustomAttributes(typeof(DatevFormatAttribute), false)
                                .Cast<DatevFormatAttribute>()
                                .FirstOrDefault()?.Format
                })
                .OrderBy(p => p.Position)
                .ToList();

            var values = props.Select(p =>
            {
                var val = p.Property.GetValue(record);
                if (p.Format != null && val is IFormattable formattable)
                    return new FormattedString(formattable.ToString(p.Format, CultureInfo.InvariantCulture));
                return val;
            });

            WriteRecordValues(values);
        }

        public void WriteRecordValues(IEnumerable<object?> values)
        {
            bool first = true;
            foreach (var value in values)
            {
                if (!first)
                    _writer.Write(_delimiter);
                first = false;

                if (value == null)
                {
                    // DATEV: null → unquoted leer
                    _writer.Write(string.Empty);
                }
                else if (value is string s)
                {
                    if (s == string.Empty)
                    {
                        // DATEV: explizit leerer String → ""
                        _writer.Write("\"\"");
                    }
                    else
                    {
                        _writer.Write($"\"{s}\"");
                    }
                }
                else if (value is FormattedString formatted)
                {
                    // DATEV: Formatierte Strings → unquoted
                    _writer.Write(formatted.Value);
                }
                else if (value is bool b)
                {
                    // DATEV: bool → unquoted (true/false)
                    _writer.Write(b ? "true" : "false");
                }
                else if (value is DateTime || value is DateTimeOffset)
                {
                    // DATEV: Datum immer unquoted (Formatierung über DatevFormatAttribute)
                    _writer.Write(value.ToString());
                }
                else
                {
                    // Andere Typen (Zahlen etc.) → unquoted
                    _writer.Write(Convert.ToString(value, CultureInfo.InvariantCulture));
                }
            }
            _writer.WriteLine();
        }

        public void Dispose() => _writer?.Dispose();
    }

    internal class FormattedString
    {
        public string Value { get; set; }
        public FormattedString(string value)
        {
            Value = value;
        }
    }
}
