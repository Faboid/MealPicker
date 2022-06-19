using MealPicker.Core.Files;
using MealPicker.Core.Services;
using MealPicker.UI.WPF.Pages.Interface;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.Pages {
    /// <summary>
    /// Interaction logic for KeyHandlerPage.xaml
    /// </summary>
    public partial class KeyHandlerPage : Page {

        private IForm current;
        public event EventHandler<ConnectionService>? CloseAndReturn;

        public KeyHandlerPage() {
            InitializeComponent();

            if(!KeyHandler.KeyMightExist()) {
                NewKeyFormHandler();
                return;
            }

            var form = new InsertPasswordForm();
            form.OnMissingKey += Form_OnMissingKey;
            form.OnExpiredKey += Form_OnExpiredKey;

            current = form;
            PageContainer.Navigate(current);
        }

        private void Form_OnExpiredKey(object? sender, EventArgs e) {
            NewKeyFormHandler();
        }

        private void Form_OnMissingKey(object? sender, EventArgs e) {
            NewKeyFormHandler();
        }

        [MemberNotNull(nameof(current))]
        private void NewKeyFormHandler() {
            //NewKeyForm will be used then no apikey is saved
            current = new NewKeyForm();
            PageContainer.Navigate(current);
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e) {
            var result = await current.ConfirmAsync();
            if(result.Result() != Utils.Options.OptionResult.Some) {
                //do nothing if it fails. Might consider adding some sort of form-wide popup, but it's handled by the IForm currently.
                return;
            }

            //since it's OptionResult.Some(or it would've returned early) the exception won't ever be hit.
            //might do this differently in a future update
            var conn = result.Match(
                    some => some,
                    () => throw new Exception()
                );

            CloseAndReturn?.Invoke(this, conn);
        }
    }
}
