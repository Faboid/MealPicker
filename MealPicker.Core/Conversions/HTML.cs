using System.Text.RegularExpressions;

namespace MealPicker.Core.Conversions; 

/// <summary>
/// Provides HTML-related static methods.
/// </summary>
public static class HTML {

    //regular expression by vfilby on https://stackoverflow.com/questions/286813/how-do-you-convert-html-to-plain-text
    private readonly static Regex tags = new("<[^>]*>", RegexOptions.Compiled);

    /// <summary>
    /// Erases all HTML markup and returns a plain text string.
    /// </summary>
    /// <param name="HTML_Text">The string to clean from HTML markup.</param>
    /// <returns>A plain text string.</returns>
    public static string ToPlainText(string HTML_Text) {
        return tags.Replace(HTML_Text, string.Empty);
    }

}
