using MealPickerLibrary.Files;
using MealPickerLibrary.Generic;
using MealPickerUI.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MealPickerUI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        MainWindow window = new MainWindow();

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            //to receive english exception messages when debugging
            Settings.IFDEBUGSetCurrentThreadToEnglish();

            if(API_Key.Check() is false) {
                KeyArchiver keyArchiver = new KeyArchiver();
                keyArchiver.Closing += KeyArchiver_Closing;
                keyArchiver.Show();
                //window.Visibility = Visibility.Hidden;
            }

            window.Show();
        }

        private void KeyArchiver_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            window.Visibility = Visibility.Visible;
        }
    }
}
