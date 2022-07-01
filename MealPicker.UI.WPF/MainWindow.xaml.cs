using MealPicker.Core;
using MealPicker.Core.Services;
using MealPicker.UI.WPF.Pages;
using MealPicker.Utils;
using System.Windows;

namespace MealPicker.UI.WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly ILogger logger = new Logger();

        public MainWindow() {
            InitializeComponent();

            KeyHandlerPage page = new(logger);
            page.OnSendMessage += Page_OnSendMessage;
            page.CloseAndReturn += OnConnectionObtained;
            PageContainer.Navigate(page);
        }

        private void Page_OnSendMessage(object? sender, string e) {
            ShowMessageBox(e);
        }

        private void OnConnectionObtained(object? sender, IConnectionService e) {
            RecipesNavigator nav = new(logger, e);
            Dispatcher.Invoke(() => {
                RecipePage page = new(nav);
                PageContainer.Navigate(page);
            });
        }

        private void ShowMessageBox(string message) {
            MessageBoxMessageTextBlock.Text = message;
            MessageBox.Visibility = Visibility.Visible;
        }

        private void MessageBoxOkButton_Click(object sender, RoutedEventArgs e) {
            MessageBoxMessageTextBlock.Text = "";
            MessageBox.Visibility = Visibility.Hidden;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void ResizeButton_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState switch {
                WindowState.Normal => WindowState.Maximized,
                WindowState.Maximized => WindowState.Normal,
                _ => WindowState.Normal
            };
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

    }
}
