using System;

namespace DatevSharp.CSV.Helper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DatevPositionAttribute : Attribute
    {
        public int Position { get; }

        public DatevPositionAttribute(int position)
        {
            if (position < 0) throw new ArgumentOutOfRangeException(nameof(position));
            Position = position;
        }
    }
}
