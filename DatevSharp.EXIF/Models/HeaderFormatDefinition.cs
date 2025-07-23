namespace DatevSharp.EXIF.Models
{
    /// <summary>
    /// Enthält die Formatdefinition für eine bestimmte DATEV-Formatkategorie.
    /// </summary>
    public class HeaderFormatDefinition
    {
        public Formatkategorie Kategorie { get; }
        public string Formatname { get; }
        public int Formatversion { get; }
        public int? Sachkontenlaenge { get; }

        public HeaderFormatDefinition(
            Formatkategorie kategorie,
            string formatname,
            int formatversion,
            int? sachkontenlaenge = null)
        {
            Kategorie = kategorie;
            Formatname = formatname;
            Formatversion = formatversion;
            Sachkontenlaenge = sachkontenlaenge;
        }
    }
}
