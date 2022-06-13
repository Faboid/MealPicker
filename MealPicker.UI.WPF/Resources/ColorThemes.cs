using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources {
    public class ColorThemes : IColorTheme {

        public static ColorThemes Singleton { get; private set; } = new(ColorTheme.Dark);

        SolidColorBrush IColorTheme.DeepestBG => _theme.DeepestBG;

        SolidColorBrush IColorTheme.DeepBG => _theme.DeepBG;

        SolidColorBrush IColorTheme.ForeBG => _theme.ForeBG;

        SolidColorBrush IColorTheme.LightText => _theme.LightText;

        private ColorThemes(ColorTheme theme) {
            Change(theme);
        }

        private IColorTheme _theme;

        [MemberNotNull(nameof(_theme))]
        public void Change(ColorTheme theme) {
            _theme = theme switch {
                ColorTheme.Dark => new DarkTheme(),
                _ => new DarkTheme(), //default
            };

            OnChangedTheme?.Invoke(this, _theme);
        }

        public event EventHandler<IColorTheme>? OnChangedTheme;

    }

    public enum ColorTheme {
        Dark,
    }

    public class DarkTheme : IColorTheme {

        SolidColorBrush IColorTheme.DeepestBG => throw new NotImplementedException();
        SolidColorBrush IColorTheme.DeepBG => throw new NotImplementedException();
        SolidColorBrush IColorTheme.ForeBG => throw new NotImplementedException();
        SolidColorBrush IColorTheme.LightText => throw new NotImplementedException();

    }

    public interface IColorTheme {

        //temp names
        SolidColorBrush DeepestBG { get; }
        SolidColorBrush DeepBG { get; }
        SolidColorBrush ForeBG { get; }
        SolidColorBrush LightText { get; }

    }

    internal static class Extensions {

        //todo - test this method
        public static SolidColorBrush ToBrush(string hex) {

            if(hex.Length == 6) {
                var color = Color
                    .FromRgb(
                        Convert.ToByte(hex.Substring(0, 2), 16),
                        Convert.ToByte(hex.Substring(2, 2), 16),
                        Convert.ToByte(hex.Substring(4, 2), 16)
                    );
                return new(color);
            }

            if(hex.Length == 8) {
                var color = Color
                    .FromArgb(
                        Convert.ToByte(hex.Substring(0, 2), 16),
                        Convert.ToByte(hex.Substring(2, 2), 16),
                        Convert.ToByte(hex.Substring(4, 2), 16),
                        Convert.ToByte(hex.Substring(6, 2), 16)
                    );
                return new(color);
            }

            throw new ArgumentException($"The given hex ({hex}) is not valid.", nameof(hex));
        }

    }

}
