using MealPicker.Core.Files;
using MealPicker.Core.Services;
using MealPicker.Encryption;
using MealPicker.UI.WPF.Pages.Interface;
using MealPicker.Utils;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.Pages {
    /// <summary>
    /// Interaction logic for NewKeyForm.xaml
    /// </summary>
    public partial class NewKeyForm : Page, IForm {
        public NewKeyForm() {
            InitializeComponent();
        }

        public async Task<Option<ConnectionService>> ConfirmAsync() {

            //todo - implement error message
            if(PasswordTextBox.Text != ConfirmPasswordTextBox.Text) {
                return Option.None<ConnectionService>();
            }

            CryptoService cryptoService = new(PasswordTextBox.Text.ToCharArray());
            using KeyHandler handler = new(cryptoService);
            var result = await handler.TrySet(APIKeyTextBox.Text);

            return result
                .Match(
                    some => some,
                    error => DisplayErrors(error),
                    () => Option.None<ConnectionService>()
                );
        }

        private Option<ConnectionService> DisplayErrors(KeyHandler.KeyError failResult) {

            var stuff = failResult switch {
                KeyHandler.KeyError.Timeout => "The key check has timed out.",
                KeyHandler.KeyError.InvalidOrExpiredKey => "The given key is invalid.",
                _ => "The verification process has failed for an unknown reason. Please check everything is correct and retry again."
            };

            //todo - display the error

            return Option.None<ConnectionService>();
        }

    }
}
