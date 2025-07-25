using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DatevSharp.CSV.Helper
{
    public class DatevCsvReader : IDisposable
    {
        private readonly TextReader _reader;
        private readonly string _delimiter;
        private object? _cachedHeader;
        private bool _headerRead = false;

        public DatevCsvReader(TextReader reader, string delimiter = ";")
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _delimiter = delimiter;
        }

        /// <summary>
        /// Liest und cached die erste Zeile als DatevHeader.
        /// Kann beliebig oft abgerufen werden, ohne den Stream erneut zu lesen.
        /// </summary>
        public T GetHeader<T>() where T : new()
        {
            if (_headerRead && _cachedHeader is T typedHeader)
                return typedHeader;

            // Erste Zeile lesen und mappen
            var headerLine = _reader.ReadLine();
            if (headerLine == null)
                throw new InvalidOperationException("CSV enthält keine Header-Zeile.");

            var headerFields = SplitCsvLine(headerLine);
            var header = MapToObject<T>(headerFields);

            _cachedHeader = header;
            _headerRead = true;

            return header;
        }

        /// <summary>
        /// Liest eine Zeile und gibt sie als Liste von Strings/Null zurück.
        /// </summary>
        public List<string?>? ReadLine()
        {
            var line = _reader.ReadLine();
            if (line == null)
                return null;

            return SplitCsvLine(line);
        }

        /// <summary>
        /// Liest eine Zeile und mapped sie auf ein Objekt vom Typ T.
        /// </summary>
        public T ReadRecord<T>() where T : new()
        {
            var fields = ReadLine();
            if (fields == null) return default;
            return MapToObject<T>(fields);
        }

        /// <summary>
        /// Liest alle verbleibenden Zeilen und mapped sie auf T.
        /// </summary>
        public IEnumerable<T> ReadRecords<T>() where T : new()
        {
            List<string?>? line;
            while ((line = ReadLine()) != null)
            {
                if (line.Count == 0 || line.All(string.IsNullOrWhiteSpace))
                    continue;
                yield return MapToObject<T>(line);
            }
        }

        private T MapToObject<T>(List<string?> fields) where T : new()
        {
            var obj = new T();

            var props = typeof(T).GetProperties()
                .Select(p => new
                {
                    Property = p,
                    Position = p.GetCustomAttributes(typeof(DatevPositionAttribute), false)
                                .Cast<DatevPositionAttribute>()
                                .FirstOrDefault()?.Position ?? int.MaxValue,
                    Format = p.GetCustomAttributes(typeof(DatevFormatAttribute), false)
                                .Cast<DatevFormatAttribute>()
                                .FirstOrDefault()?.Format
                })
                .OrderBy(p => p.Position)
                .ToArray();

            for (int i = 0; i < props.Length && i < fields.Count; i++)
            {
                var val = fields[i];
                if (val == null)
                {
                    props[i].Property.SetValue(obj, null);
                    continue;
                }

                var targetType = Nullable.GetUnderlyingType(props[i].Property.PropertyType) ?? props[i].Property.PropertyType;

                if (targetType == typeof(string))
                {
                    if (val == string.Empty)
                    {
                        props[i].Property.SetValue(obj, null);
                    }
                    else
                    {
                        if (val.StartsWith("\"") && val.EndsWith("\""))
                            val = val[1..^1];
                        props[i].Property.SetValue(obj, val);
                    }
                }
                else if (targetType == typeof(DateTime))
                {
                    if (string.IsNullOrEmpty(val))
                        props[i].Property.SetValue(obj, null);
                    else
                        props[i].Property.SetValue(obj, DateTime.ParseExact(val, props[i].Format ?? "yyyyMMdd", CultureInfo.InvariantCulture));
                }
                else if (targetType == typeof(int))
                {
                    props[i].Property.SetValue(obj, string.IsNullOrEmpty(val) ? null : Convert.ToInt32(val, CultureInfo.InvariantCulture));
                }
                else if (targetType == typeof(decimal))
                {
                    props[i].Property.SetValue(obj, string.IsNullOrEmpty(val) ? null : Convert.ToDecimal(val, CultureInfo.InvariantCulture));
                }
            }

            return obj;
        }

        /// <summary>
        /// Zerlegt eine CSV-Zeile und unterscheidet zwischen null (;) und "" (gequotet leer).
        /// </summary>
        private List<string> SplitCsvLine(string line) => line.Split(";").ToList();
        //{
        //    var result = new List<string?>();
        //    bool inQuotes = false;
        //    var current = "";

        //    for (int i = 0; i < line.Length; i++)
        //    {
        //        var c = line[i];

        //        if (c == '"')
        //        {
        //            inQuotes = !inQuotes;
        //            continue;
        //        }

        //        if (c == _delimiter[0] && !inQuotes)
        //        {
        //            result.Add(ProcessField(current));
        //            current = "";
        //        }
        //        else
        //        {
        //            current += c;
        //        }
        //    }
        //    result.Add(ProcessField(current)); // letztes Feld
        //    return result;
        //}

        private string? ProcessField(string raw)
        {
            if (raw == string.Empty)
                return null;           // unquoted leer
            if (raw == "\"\"")
                return string.Empty;   // explizit gequoted leer
            return raw;
        }

        public void Dispose()
        {
            _reader?.Dispose();
        }
    }
}
