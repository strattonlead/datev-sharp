using CreateIf.Datev.Models;
using DatevSharp.EXIF.Models;
using System;
using System.Collections.Generic;

namespace CreateIf.Datev.Services
{
    public static class DatevHeaderFactory
    {
        private static readonly Dictionary<Formatkategorie, HeaderFormatDefinition> _definitions =
            new Dictionary<Formatkategorie, HeaderFormatDefinition>()
            {
                {
                    Formatkategorie.DebitorenKreditoren,
                    new HeaderFormatDefinition(
                        Formatkategorie.DebitorenKreditoren,
                        "\"Debitoren/Kreditoren\"",
                        5,
                        4 // Pflicht
                    )
                },
                {
                    Formatkategorie.Kontenbeschriftungen,
                    new HeaderFormatDefinition(
                        Formatkategorie.Kontenbeschriftungen,
                        "\"Kontenbeschriftungen\"",
                        2,
                        4
                    )
                },
                {
                    Formatkategorie.Buchungsstapel,
                    new HeaderFormatDefinition(
                        Formatkategorie.Buchungsstapel,
                        "\"Buchungsstapel\"",
                        13,
                        4
                    )
                },
                {
                    Formatkategorie.WiederkehrendeBuchungen,
                    new HeaderFormatDefinition(
                        Formatkategorie.WiederkehrendeBuchungen,
                        "\"Wiederkehrende Buchungen\"",
                        13,
                        4
                    )
                },
                {
                    Formatkategorie.Zahlungsbedingungen,
                    new HeaderFormatDefinition(
                        Formatkategorie.Zahlungsbedingungen,
                        "\"Zahlungsbedingungen\"",
                        4
                    )
                },
                {
                    Formatkategorie.DiverseAdressen,
                    new HeaderFormatDefinition(
                        Formatkategorie.DiverseAdressen,
                        "\"Adressen\"",
                        2
                    )
                }
            };

        public static HeaderFormatDefinition GetDefinition(Formatkategorie kategorie)
        {
            if (!_definitions.TryGetValue(kategorie, out var def))
                throw new ArgumentOutOfRangeException(nameof(kategorie), $"Kein Header-Format für Formatkategorie {kategorie} definiert.");

            return def;
        }

        public static DatevHeader CreateHeader(Formatkategorie kategorie, int beraterNr, int mandantNr, string bezeichnung = "")
        {
            var def = DatevHeaderFactory.GetDefinition(kategorie);

            return new DatevHeader
            {
                Formatkategorie = (int)def.Kategorie,
                Formatname = def.Formatname,
                Formatversion = def.Formatversion,
                Sachkontenlaenge = def.Sachkontenlaenge ?? 0,
                Beraternummer = beraterNr,
                Mandantennummer = mandantNr,
                Wirtschaftsjahresbeginn = DateTime.Today.ToString("yyyyMMdd"),
                ErzeugtAm = DateTime.Now,
                Bezeichnung = $"\"{(string.IsNullOrWhiteSpace(bezeichnung) ? def.Formatname.Trim('"') : bezeichnung)}\""
            };
        }
    }
}
