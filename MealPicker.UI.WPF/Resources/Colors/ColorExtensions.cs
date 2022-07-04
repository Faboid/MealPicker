using System;
using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources.Colors {
    internal static class ColorExtensions {

        /// <summary>
        /// Sets the <paramref name="brush"/>'s opacity to <paramref name="value"/>.
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SolidColorBrush WithOpacity(this SolidColorBrush brush, double value) {
            brush.Opacity = value;
            return brush;
        }

        /// <summary>
        /// Creates a <see cref="SolidColorBrush"/> representation of the given <paramref name="hex"/> value. <br/><br/>
        /// Throws <see cref="ArgumentOutOfRangeException"/> when the length of <paramref name="hex"/> is less than 6 or more than 8(9 if it starts with #).
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static SolidColorBrush ToSolidBrush(this string hex) {
            return new(hex.ToColor());
        }

        /// <summary>
        /// Creates a <see cref="Color"/> representation of the given <paramref name="hex"/> value. <br/><br/>
        /// Throws <see cref="ArgumentOutOfRangeException"/> when the length of <paramref name="hex"/> is less than 6 or more than 8(9 if it starts with #).
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Color ToColor(this string hex) {
            
            if(hex[0] == '#') {
                hex = hex[1..];
            }

            if(hex.Length == 6) {
                var color = Color
                    .FromRgb(
                        Convert.ToByte(hex.Substring(0, 2), 16),
                        Convert.ToByte(hex.Substring(2, 2), 16),
                        Convert.ToByte(hex.Substring(4, 2), 16)
                    );
                return color;
            }

            if(hex.Length == 8) {
                var color = Color
                    .FromArgb(
                        Convert.ToByte(hex.Substring(0, 2), 16),
                        Convert.ToByte(hex.Substring(2, 2), 16),
                        Convert.ToByte(hex.Substring(4, 2), 16),
                        Convert.ToByte(hex.Substring(6, 2), 16)
                    );
                return color;
            }

            throw new ArgumentOutOfRangeException($"The given hex ({hex}) is bigger than the maximum hex size.", nameof(hex));
        }

    }
}
