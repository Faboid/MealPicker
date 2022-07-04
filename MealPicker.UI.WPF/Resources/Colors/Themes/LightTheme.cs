using System;
using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources.Colors.Themes;

/// <summary>
/// Represents a light theme.
/// </summary>
public class LightTheme : IColorTheme {
    public SolidColorBrush BG_100 { get; } = "#e1e1e1".ToSolidBrush();
    public SolidColorBrush BG_200 { get; } = "#ececec".ToSolidBrush();
    public SolidColorBrush BG_300 { get; } = "#f6f6f6".ToSolidBrush();
    public SolidColorBrush BG_400 { get; } = "#ffffff".ToSolidBrush();

    public SolidColorBrush FG_100 { get; } = "#000000".ToSolidBrush();
    public SolidColorBrush FG_200 { get; } = "#000000".ToSolidBrush();
    public SolidColorBrush FG_300 { get; } = "#000000".ToSolidBrush();
    public SolidColorBrush FG_400 { get; } = "#000000".ToSolidBrush();

    public SolidColorBrush Primary_100 { get; } = "#d0d0d0".ToSolidBrush();

    public SolidColorBrush Highlight_100 { get; } = "#606060".ToSolidBrush();
    public SolidColorBrush Highlight_200 { get; } = "#696969".ToSolidBrush();

    public SolidColorBrush Disabled_100 { get; } = "#d0d0d0".ToSolidBrush();
}