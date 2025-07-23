using CreateIf.Datev.Validation;
using DatevSharp.EXIF.Models;

namespace DatevSharp.Tests
{
    public class DebitorKreditorValidatorTests
    {
        [Fact]
        public void Should_Validate_Valid_Debitor()
        {
            var d = new DebitorKreditor
            {
                Konto = "10001",
                Kurzbezeichnung = "\"TEST\"",
                Land = "\"DE\"",
                IBAN1 = "\"DE44500105175407324931\""
            };

            var validator = new DebitorKreditorValidator();
            var result = validator.Validate(d);

            Assert.Empty(result);
        }

        [Fact]
        public void Should_Detect_Invalid_Konto_And_IBAN()
        {
            var d = new DebitorKreditor
            {
                Konto = "abc",
                Kurzbezeichnung = "\"Test\"",
                Land = "D",
                IBAN1 = "\"123\"",
                SWIFT1 = "\"123456789012\""
            };

            var validator = new DebitorKreditorValidator();
            var errors = validator.Validate(d);

            Assert.Contains("Konto muss 4 bis 9 Ziffern enthalten.", errors);
            Assert.Contains("IBAN1 ist ungültig.", errors);
            Assert.Contains("SWIFT1 darf maximal 11 Zeichen enthalten.", errors);
            Assert.Contains("Land muss ein zweistelliger ISO-Code sein (z. B. \"DE\").", errors);
        }
    }
}
