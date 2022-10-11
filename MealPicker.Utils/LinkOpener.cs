using System.ComponentModel;
using System.Diagnostics;

namespace MealPicker.Utils;

public class LinkOpener : ILinkOpener {

    public void Open(string url) {
        if(string.IsNullOrWhiteSpace(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute)) {
            return;
        }

        try {
            Process.Start(new ProcessStartInfo() {
                UseShellExecute = true,
                FileName = url
            });
        } catch(Win32Exception) { } //thrown when given an unvalid url.
    }

}