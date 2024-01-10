namespace CW_ALM.Fluent.Common
{
    public static class LocalizerSettings
    {
        public const string NeutralCulture = "pt-BR";

        public static readonly string[] SupportedCultures = { NeutralCulture, "en-US", "es-CO", "es-PE" };

        public static readonly (string, string)[] SupportedCulturesWithName = new[] { ("PT-BR", NeutralCulture), ("EN-US", "en-US"), ("ES-PE", "es-PE"), ("ES-CO", "es-CO") };
    }
}
