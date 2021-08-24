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


        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            //to receive english exception messages when debugging
            Settings.IFDEBUGSetCurrentThreadToEnglish();

            if(API_Key.Check() is false) {
                KeyArchiver keyArchiver = new KeyArchiver();
                keyArchiver.Show();
            } else {
                MainWindow window = new MainWindow();
                window.Show();
            }
            
        }

    }
}
