using MealPicker.Core.Files;
using MealPicker.Core.Services;
using MealPicker.UI.WPF.Pages.Interface;
using MealPicker.Utils;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.Pages {
    /// <summary>
    /// Interaction logic for KeyHandlerPage.xaml
    /// </summary>
    public partial class KeyHandlerPage : Page {

        //todo - introduct disposing protocol for subscribed events

        private IForm current;
        public event EventHandler<string>? OnSendMessage;
        public event EventHandler<IConnectionService>? CloseAndReturn;

        private readonly ILogger logger;

        public KeyHandlerPage(ILogger logger) {
            InitializeComponent();
            this.logger = logger;

            if(!KeyHandler.KeyMightExist()) {
                NewKeyFormHandler();
                return;
            }

            var form = new InsertPasswordForm(logger);
            form.OnSendMessage += (a, b) => OnSendMessage?.Invoke(a, b);
            form.OnMissingKey += Form_OnMissingKey;
            form.OnExpiredKey += Form_OnExpiredKey;

            current = form;
            PageContainer.Navigate(current);
        }

        private void Form_OnExpiredKey(object? sender, EventArgs e) {
            OnSendMessage?.Invoke(this, "The previously given key has expired. Please insert the updated one.");
            NewKeyFormHandler();
        }

        private void Form_OnMissingKey(object? sender, EventArgs e) {
            OnSendMessage?.Invoke(this, "The key given previously is missing. Please insert it once more.");
            NewKeyFormHandler();
        }

        [MemberNotNull(nameof(current))]
        private void NewKeyFormHandler() {
            var form = new NewKeyForm(logger);
            form.OnSendMessage += (a, b) => OnSendMessage?.Invoke(a, b);
            current = form;
            PageContainer.Navigate(current);
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e) {

            try {

                ConfirmButton.IsEnabled = false;

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

            } finally {
                
                ConfirmButton.IsEnabled = true;

            }

        }
    }
}
