using CreateIf.Datev.Models;
using CreateIf.Datev.Services;
using DatevSharp.EXIF.Models;
using System;

namespace DatevSharp.EXIF.Services
{
    public static class DatevHeaderBuilder
    {
        public static DatevHeader Create(
            Formatkategorie formatkategorie,
            int beraternummer,
            int mandantennummer,
            string bezeichnung = null)
        {
            var def = DatevHeaderFactory.GetDefinition(formatkategorie);
            var now = DateTime.Now;

            return new DatevHeader
            {
                Kennzeichen = "\"EXTF\"",
                Versionsnummer = 700,
                Formatkategorie = (int)def.Kategorie,
                Formatname = def.Formatname,
                Formatversion = def.Formatversion,
                ErzeugtAm = now,
                Herkunft = "\"RE\"",
                ExportiertVon = "\"CreateIf\"",
                ImportiertVon = "\"\"",
                Beraternummer = beraternummer,
                Mandantennummer = mandantennummer,
                Wirtschaftsjahresbeginn = new DateTime(now.Year, 1, 1),
                Sachkontenlaenge = def.Sachkontenlaenge ?? 0,
                Bezeichnung = $"\"{(string.IsNullOrWhiteSpace(bezeichnung) ? def.Formatname.Trim('"') : bezeichnung)}\"",
                Diktatkürzel = "\"WD\"",
                Buchungstyp = 1,
                Rechnungslegungszweck = 0,
                Festschreibung = 0,
                Waehrung = "\"EUR\"",
                Derivatskennzeichen = "\"\"",
                Sachkontenrahmen = "\"03\"",
                Anwendungsinformation = "\"\""
            };
        }
    }
}
