using MealPicker.Core.Files;
using MealPicker.Core.Services;
using MealPicker.Encryption;
using MealPicker.UI.WPF.Pages.Interface;
using MealPicker.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.Pages {
    /// <summary>
    /// Interaction logic for NewKeyForm.xaml
    /// </summary>
    public partial class NewKeyForm : Page, IForm {

        public event EventHandler<string>? OnSendMessage;

        public NewKeyForm() {
            InitializeComponent();
        }

        public async Task<Option<IConnectionService>> ConfirmAsync() {

            //todo - implement error message
            if(PasswordTextBox.Text != ConfirmPasswordTextBox.Text) {
                OnSendMessage?.Invoke(this, "The passwords must be equal.");
                return Option.None<IConnectionService>();
            }

            CryptoService cryptoService = new(PasswordTextBox.Text.ToCharArray());
            using KeyHandler handler = new(cryptoService);
            var result = await handler.TrySet(APIKeyTextBox.Text);

            return result
                .Match(
                    some => Option.Some(some),
                    error => DisplayErrors(error),
                    () => Option.None<IConnectionService>()
                );
        }

        private Option<IConnectionService> DisplayErrors(KeyHandler.KeyError failResult) {

            var errorMessage = failResult switch {
                KeyHandler.KeyError.Timeout => "The key check has timed out.",
                KeyHandler.KeyError.InvalidOrExpiredKey => "The given key is invalid.",
                _ => "The verification process has failed for an unknown reason. Please check everything is correct and retry again."
            };

            OnSendMessage?.Invoke(this, errorMessage);

            return Option.None<IConnectionService>();
        }

    }
}
