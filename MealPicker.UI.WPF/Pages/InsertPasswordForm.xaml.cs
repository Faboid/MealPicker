using MealPicker.Core.Files;
using MealPicker.Core.Services;
using MealPicker.Encryption;
using MealPicker.UI.WPF.Pages.Interface;
using MealPicker.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.Pages; 

/// <summary>
/// Interaction logic for InsertPasswordForm.xaml
/// </summary>
public partial class InsertPasswordForm : Page, IForm {

    public event EventHandler? OnMissingKey;
    public event EventHandler? OnExpiredKey;

    public InsertPasswordForm() {
        InitializeComponent();
    }

    public async Task<Option<ConnectionService>> ConfirmAsync() {
        CryptoService crypto = new(PasswordTextBox.Text.ToCharArray());
        using KeyHandler keyHandler = new(crypto);
        var result = await keyHandler.TryGet();

        return result.Match<Option<ConnectionService>>(
            some => some,
            error => DisplayErrors(error),
            () => Option.None<ConnectionService>()
        );
    }

    private Option<ConnectionService> DisplayErrors(KeyHandler.KeyError failResult) {

        if(failResult == KeyHandler.KeyError.MissingKey) {
            OnMissingKey?.Invoke(this, EventArgs.Empty);
        }

        if(failResult == KeyHandler.KeyError.InvalidOrExpiredKey) {
            OnExpiredKey?.Invoke(this, EventArgs.Empty);
        }

        var stuff = failResult switch {
            KeyHandler.KeyError.Timeout => "The key check has timed out.",
            KeyHandler.KeyError.InvalidOrExpiredKey => "The given key has expired.",
            KeyHandler.KeyError.WrongPassword => "Wrong password.",
            KeyHandler.KeyError.MissingKey => "The key is missing.",
            _ => "The verification process has failed for an unknown reason. Please check everything is correct and retry again."
        };

        //todo - display the error

        return Option.None<ConnectionService>();
    }

}
