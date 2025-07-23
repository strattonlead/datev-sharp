using CreateIf.Datev.Models;

namespace DatevSharp.Tests
{
    public class DatevHeaderTests
    {
        [Fact]
        public void Should_Initialize_Header_With_Default_Values()
        {
            var header = new DatevHeader
            {
                Formatkategorie = 21,
                Formatversion = 13,
                Beraternummer = 1234,
                Mandantennummer = 56789,
                Wirtschaftsjahresbeginn = "20250101",
                Sachkontenlaenge = 4
            };

            Assert.Equal("\"EXTF\"", header.Kennzeichen);
            Assert.Equal(700, header.Versionsnummer);
            Assert.Equal(21, header.Formatkategorie);
            Assert.Equal("\"Buchungsstapel\"", header.Formatname);
            Assert.Equal(13, header.Formatversion);
            Assert.Equal("\"EUR\"", header.Waehrung);
        }
    }
}