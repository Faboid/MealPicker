using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources.Colors.Themes;

public interface IColorTheme {

    //temp names
    SolidColorBrush BG_100 { get; }
    SolidColorBrush BG_200 { get; }
    SolidColorBrush BG_300 { get; }
    SolidColorBrush BG_400 { get; }

    SolidColorBrush FG_100 { get; }
    SolidColorBrush FG_200 { get; }
    SolidColorBrush FG_300 { get; }
    SolidColorBrush FG_400 { get; }

    SolidColorBrush Primary_100 { get; }

    SolidColorBrush Highlight_100 { get; }
    SolidColorBrush Highlight_200 { get; }

    SolidColorBrush Disabled_100 { get; }

}

