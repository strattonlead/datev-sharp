using CreateIf.Datev.Models;
using CreateIf.Datev.Validation;
using DatevSharp.EXIF.Models;
using DatevSharp.EXIF.Services;

namespace DatevSharp.Tests
{
    public class DatevHeaderValidatorTests
    {
        [Fact]
        public void Should_Pass_With_Valid_Buchungsstapel_Header()
        {
            var header = DatevHeaderBuilder.Create(
                Formatkategorie.Buchungsstapel,
                beraternummer: 1234,
                mandantennummer: 56789
            );

            var validator = new DatevHeaderValidator();
            var result = validator.Validate(header);

            Assert.Empty(result);
        }

        [Fact]
        public void Should_Report_Errors_For_Invalid_Header()
        {
            var header = new DatevHeader
            {
                Kennzeichen = "\"XYZ\"",
                Versionsnummer = 123,
                Formatkategorie = 99, // ungültig
                Formatname = "\"Falsch\"",
                Formatversion = 1,
                Beraternummer = 0,
                Mandantennummer = 0,
                Wirtschaftsjahresbeginn = "2025-01-01"
            };

            var validator = new DatevHeaderValidator();
            var result = validator.Validate(header);

            Assert.Contains(result, x => x.Contains("Kennzeichen"));
            Assert.Contains(result, x => x.Contains("Versionsnummer"));
            Assert.Contains(result, x => x.Contains("Beraternummer"));
            Assert.Contains(result, x => x.Contains("Mandantennummer"));
            Assert.Contains(result, x => x.Contains("Wirtschaftsjahresbeginn"));
            Assert.Contains(result, x => x.Contains("Unbekannte Formatkategorie"));
        }
    }
}
