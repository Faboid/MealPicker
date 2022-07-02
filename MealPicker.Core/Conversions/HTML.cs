using System.Text.RegularExpressions;

namespace MealPicker.Core.Conversions; 

public static class HTML {

    //regular expression by vfilby on https://stackoverflow.com/questions/286813/how-do-you-convert-html-to-plain-text
    private readonly static Regex tags = new("<[^>]*>", RegexOptions.Compiled);

    public static string ToPlainText(string HTML_Text) {
        return tags.Replace(HTML_Text, string.Empty);
    }

}
