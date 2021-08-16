using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MealPickerLibrary.Conversions {
    public static class HTMLtoPlainText {

        public static string Convert(string HTML_Text) {
            //regular expression by vfilby on https://stackoverflow.com/questions/286813/how-do-you-convert-html-to-plain-text
            Regex tags = new Regex("<[^>]*>");

            return tags.Replace(HTML_Text, string.Empty);
        }

    }
}
