using MealPickerLibrary.Generic;
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

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            //to receive english exception messages when debugging
            Settings.IFDEBUGSetCurrentThreadToEnglish();

            MainWindow window = new MainWindow();
            window.Show();
        }

    }
}
