using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MealPickerLibrary.Files;
using MealPickerLibrary.Queries;

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
                this.Close();
            } else {
                DarkMessageBox.Show("Error!", "Something went wrong.", Dispatcher);
            }

        }
    }
}
