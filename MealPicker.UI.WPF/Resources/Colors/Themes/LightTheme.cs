using System;
using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources.Colors.Themes;

public class LightTheme : IColorTheme {
    public Color DeepestBG => "FFFFFFFF".ToColor();
    public Color DeepBG => throw new NotImplementedException();
    public Color ForeBG => throw new NotImplementedException();
    public Color LightText => "FF272121".ToColor();

}