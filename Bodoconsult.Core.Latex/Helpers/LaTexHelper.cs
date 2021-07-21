namespace Bodoconsult.Core.Latex.Helpers
{
    /// <summary>
    /// Helper class with LaTex relevante methods
    /// </summary>
    public static class LaTexHelper
    {

        /// <summary>
        /// Escape a string for LaTex
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Escape(string input)
        {
            return string.IsNullOrEmpty(input) ? "" 
                : input.Replace("&", "\\&")
                    .Replace("Ü", "\\\"U")
                    .Replace("Ä", "\\\"A")
                    .Replace("Ö", "\\\"O")
                    .Replace("ü", "\\\"u")
                    .Replace("ä", "\\\"a")
                    .Replace("ö", "\\\"o")
                    .Replace("ß", "{\\ss}")
                    .Replace("#", "\\#");
        }


    }
}
