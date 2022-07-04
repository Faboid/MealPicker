using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources.Colors.Themes;

/// <summary>
/// Represents the framework of a theme.
/// </summary>
public interface IColorTheme {

    /// <summary>
    /// Background_100
    /// </summary>
    SolidColorBrush BG_100 { get; }
    /// <summary>
    /// Background_200
    /// </summary>
    SolidColorBrush BG_200 { get; }
    /// <summary>
    /// Background_300
    /// </summary>
    SolidColorBrush BG_300 { get; }
    /// <summary>
    /// Background_400
    /// </summary>
    SolidColorBrush BG_400 { get; }


    /// <summary>
    /// Foreground_100
    /// </summary>
    SolidColorBrush FG_100 { get; }
    /// <summary>
    /// Foreground_200
    /// </summary>
    SolidColorBrush FG_200 { get; }
    /// <summary>
    /// Foreground_300
    /// </summary>
    SolidColorBrush FG_300 { get; }
    /// <summary>
    /// Foreground_400
    /// </summary>
    SolidColorBrush FG_400 { get; }

    /// <summary>
    /// Primary_100
    /// </summary>
    SolidColorBrush Primary_100 { get; }

    /// <summary>
    /// Highlight_100
    /// </summary>
    SolidColorBrush Highlight_100 { get; }
    /// <summary>
    /// Highlight_200
    /// </summary>
    SolidColorBrush Highlight_200 { get; }

    /// <summary>
    /// Disabled_100
    /// </summary>
    SolidColorBrush Disabled_100 { get; }

}

