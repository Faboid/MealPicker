using MealPicker.Core.Services;
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

            string password = PasswordTextBox.Text;

            //todo - implement error message
            if(password != ConfirmPasswordTextBox.Text) {
                return Option.None<ConnectionService>();
            }

            API_Key key = new(APIKeyTextBox.Text);
            var result = await ConnectionService.CreateConnectionAsync(key);

            return result
                .Match(
                    some => SaveAndReturnConnection(key, password, some),
                    error => DisplayErrors(error),
                    () => Option.None<ConnectionService>()
                );
        }

        private Option<ConnectionService> SaveAndReturnConnection(API_Key key, string password, ConnectionService connection) {

            //todo - save the key to file

            return connection;
        }

        private Option<ConnectionService> DisplayErrors(IConnectionService.FailResult failResult) {

            var stuff = failResult.StatusCode switch {
                HttpStatusCode.GatewayTimeout => "Failed to connect to the server.",
                HttpStatusCode.Unauthorized => "The given key is not valid.",
                _ => "The verification process has failed for an unknown reason. Please check everything is correct and retry again."
            };

            //todo - display the error

            return Option.None<ConnectionService>();
        }

    }
}
