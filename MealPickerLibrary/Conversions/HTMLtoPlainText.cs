using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MealPickerLibrary.Conversions {
    public static class HTMLtoPlainText {
        
        //regular expression by vfilby on https://stackoverflow.com/questions/286813/how-do-you-convert-html-to-plain-text
        private readonly static Regex tags = new("<[^>]*>", RegexOptions.Compiled);

        public static string Convert(string HTML_Text) {
            return tags.Replace(HTML_Text, string.Empty);
        }

    }
}
