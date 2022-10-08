using MealPicker.Core;
using MealPicker.Core.Services;
using MealPicker.UI.WPF.Pages;
using MealPicker.Utils;
using System.Windows;

namespace MealPicker.UI.WPF {

    /// <summary>
    /// The main window of the application. Everything is accessed through pages in its frame.
    /// </summary>
    public partial class MainWindow : Window {

        private readonly ILogger logger = new Logger();

        /// <summary>
        /// Initializes <see cref="MainWindow"/>.
        /// </summary>
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

        private void OnConnectionObtained(object? sender, (KeyHandlerPage handlerPage, IConnectionService cnnService) e) {
            e.handlerPage.OnSendMessage -= Page_OnSendMessage;
            e.handlerPage.CloseAndReturn -= OnConnectionObtained;

            RecipesNavigator nav = new(e.cnnService);
            Dispatcher.Invoke(() => {
                RecipePage page = new(nav);
                page.OnSendMessage += Page_OnSendMessage;
                PageContainer.NavigationService.RemoveBackEntry();
                PageContainer.Navigate(page);
            });
        }

        /// <summary>
        /// Displays the message box with a custom message.
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessageBox(string message) {
            MessageBoxMessageTextBlock.Text = message;
            MessageBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Resets the message box's text and closes it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageBoxOkButton_Click(object sender, RoutedEventArgs e) {
            MessageBoxMessageTextBlock.Text = "";
            MessageBox.Visibility = Visibility.Hidden;
        }

    }
}
