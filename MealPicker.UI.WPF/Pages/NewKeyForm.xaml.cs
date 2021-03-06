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
    /// Used to request a new API key from the user.
    /// </summary>
    public partial class NewKeyForm : Page, IForm {

        /// <summary>
        /// Is fired to send a message to the user via "sending it up the chain" to the main window.
        /// </summary>
        public event EventHandler<string>? OnSendMessage;

        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new <see cref="NewKeyForm"/> with the given <see cref="ILogger"/>.
        /// </summary>
        /// <param name="logger"></param>
        public NewKeyForm(ILogger logger) {
            InitializeComponent();
            this.logger = logger;
        }

        public async Task<Option<IConnectionService>> ConfirmAsync() {
            
            if(PasswordTextBox.Text != ConfirmPasswordTextBox.Text) {
                OnSendMessage?.Invoke(this, "The passwords must be equal.");
                return Option.None<IConnectionService>();
            }

            CryptoService cryptoService = new(PasswordTextBox.Text.ToCharArray());
            using KeyHandler handler = new(logger, cryptoService);
            var result = await handler.TrySet(APIKeyTextBox.Text);

            return result
                .Match(
                    some => Option.Some(some),
                    error => DisplayErrors(error),
                    () => Option.None<IConnectionService>()
                );
        }

        /// <summary>
        /// Converts <paramref name="failResult"/> to an user-friendly error message.
        /// </summary>
        /// <param name="failResult"></param>
        /// <returns></returns>
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
