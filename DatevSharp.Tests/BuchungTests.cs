using CreateIf.Datev.Services;
using DatevSharp.EXIF.Models;

namespace DatevSharp.Tests
{
    public class BuchungTests
    {
        [Fact]
        public void Should_Initialize_Buchung_With_ValidValues()
        {
            var buchung = new Buchung
            {
                Umsatz = 100.12m,
                SollHabenKennzeichen = "\"H\"",
                Konto = "8400",
                Gegenkonto = "1200",
                BUSchluessel = "\"1000\"",
                Belegdatum = "0101",
                Buchungstext = "\"Testbuchung\""
            };

            Assert.Equal(100.12m, buchung.Umsatz);
            Assert.Equal("\"H\"", buchung.SollHabenKennzeichen);
            Assert.Equal("8400", buchung.Konto);
        }

        [Fact]
        public void Should_Return_Definition_For_Buchungsstapel()
        {
            var def = DatevHeaderFactory.GetDefinition(Formatkategorie.Buchungsstapel);

            Assert.Equal("\"Buchungsstapel\"", def.Formatname);
            Assert.Equal(13, def.Formatversion);
            Assert.Equal(4, def.Sachkontenlaenge);
        }

    }
}
