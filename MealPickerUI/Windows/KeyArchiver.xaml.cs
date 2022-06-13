using System.Windows;
using MealPickerLibrary.Files;

namespace MealPickerUI.Windows {
    /// <summary>
    /// Interaction logic for KeyArchiver.xaml
    /// </summary>
    public partial class KeyArchiver : Window {
        public KeyArchiver() {
            InitializeComponent();
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e) {
            bool result = await API_Key.Set(API_Key_Textbox.Text);

            if(result is false) {
                DarkMessageBox.Show("The key did not work!", $"The given key ({API_Key_Textbox.Text}) failed to connect to the API. Are you sure it's correct?", Dispatcher);
                return;
            }

            if(await API_Key.Check(true)) {

                DarkMessageBox.Show("Success!", "The key has been saved successfully.", Dispatcher);
                LoadMainWindow();
                this.Close();
            } else {
                DarkMessageBox.Show("Error!", "Something went wrong.", Dispatcher);
            }

        }

        private void LoadMainWindow() {
            this.Visibility = Visibility.Hidden;
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }

    }
}
