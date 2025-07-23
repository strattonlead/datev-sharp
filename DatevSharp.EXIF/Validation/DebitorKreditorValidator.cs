using DatevSharp.EXIF.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CreateIf.Datev.Validation
{
    public class DebitorKreditorValidator
    {
        public IReadOnlyList<string> Validate(DebitorKreditor d)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(d.Konto) || !Regex.IsMatch(d.Konto, @"^\d{4,9}$"))
                errors.Add("Konto muss 4 bis 9 Ziffern enthalten.");

            if (string.IsNullOrWhiteSpace(d.Kurzbezeichnung))
                errors.Add("Kurzbezeichnung ist erforderlich.");
            else if (d.Kurzbezeichnung.Trim('"').Length > 15)
                errors.Add("Kurzbezeichnung darf maximal 15 Zeichen enthalten.");

            if (!string.IsNullOrWhiteSpace(d.IBAN1))
            {
                var iban = d.IBAN1.Trim('"').Replace(" ", "");
                if (iban.Length < 15 || iban.Length > 34 || !Regex.IsMatch(iban, @"^[A-Z0-9]+$"))
                    errors.Add("IBAN1 ist ungültig.");
            }

            if (!string.IsNullOrWhiteSpace(d.SWIFT1) && d.SWIFT1.Trim('"').Length > 11)
                errors.Add("SWIFT1 darf maximal 11 Zeichen enthalten.");

            if (string.IsNullOrWhiteSpace(d.Land) || !Regex.IsMatch(d.Land.Trim('"'), @"^[A-Z]{2}$"))
                errors.Add("Land muss ein zweistelliger ISO-Code sein (z. B. \"DE\").");

            return errors;
        }
    }
}
