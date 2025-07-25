using System;

namespace DatevSharp.CSV.Helper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DatevFormatAttribute : Attribute
    {
        public string Format { get; }

        public DatevFormatAttribute(string format)
        {
            Format = format ?? throw new ArgumentNullException(nameof(format));
        }
    }
}
