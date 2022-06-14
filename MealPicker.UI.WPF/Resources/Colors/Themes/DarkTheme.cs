using System;
using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources.Colors.Themes; 

public class DarkTheme : IColorTheme {
    public Color DeepestBG => "#FF272121".ToColor();
    public Color DeepBG => throw new NotImplementedException();
    public Color ForeBG => throw new NotImplementedException();
    public Color LightText => "FFFFFFFF".ToColor();

}
