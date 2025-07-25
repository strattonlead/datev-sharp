using CreateIf.Datev.Models;
using CreateIf.Datev.Services;
using DatevSharp.EXIF.Models;
using System;
using System.Collections.Generic;

namespace CreateIf.Datev.Validation
{
    public class DatevHeaderValidator
    {
        public IReadOnlyList<string> Validate(DatevHeader header)
        {
            var errors = new List<string>();

            // Pflichtfelder
            if (header.Kennzeichen != "\"EXTF\"" && header.Kennzeichen != "\"DTVF\"")
                errors.Add("Kennzeichen muss \"EXTF\" oder \"DTVF\" sein.");

            if (header.Versionsnummer != 700)
                errors.Add("Versionsnummer muss 700 sein.");

            if (header.Beraternummer <= 0)
                errors.Add("Beraternummer fehlt oder ist ungültig.");

            if (header.Mandantennummer <= 0)
                errors.Add("Mandantennummer fehlt oder ist ungültig.");

            // Lookup und Formatkategorie prüfen
            if (!Enum.IsDefined(typeof(Formatkategorie), header.Formatkategorie))
            {
                errors.Add($"Unbekannte Formatkategorie: {header.Formatkategorie}");
                return errors;
            }

            var expected = DatevHeaderFactory.GetDefinition((Formatkategorie)header.Formatkategorie);

            if (header.Formatname != expected.Formatname)
                errors.Add($"Formatname muss {expected.Formatname} sein.");

            if (header.Formatversion != expected.Formatversion)
                errors.Add($"Formatversion muss {expected.Formatversion} sein.");

            if (expected.Sachkontenlaenge.HasValue)
            {
                if (header.Sachkontenlaenge != expected.Sachkontenlaenge.Value)
                    errors.Add($"Sachkontenlänge muss {expected.Sachkontenlaenge.Value} sein.");
            }

            return errors;
        }
    }
}
