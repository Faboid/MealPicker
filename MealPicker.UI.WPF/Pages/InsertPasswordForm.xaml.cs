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
/// This page requests for a password to decrypt and validate the previously stored API key.
/// </summary>
public partial class InsertPasswordForm : Page, IForm {

    /// <summary>
    /// Is fired when the file with the key is missing or empty.
    /// </summary>
    public event EventHandler? OnMissingKey;

    /// <summary>
    /// Is fired when the stored API key is not valid.
    /// </summary>
    public event EventHandler? OnExpiredKey;

    /// <summary>
    /// Is fired when the user requests to change API key.
    /// </summary>
    public event EventHandler? OnChangeAPIRequest;

    /// <summary>
    /// Is fired to send a message to the user via "sending it up the chain" to the main window.
    /// </summary>
    public event EventHandler<string>? OnSendMessage;

    private readonly ILogger logger;

    public InsertPasswordForm(ILogger logger) {
        InitializeComponent();
        this.logger = logger;
    }

    public async Task<Option<IConnectionService>> ConfirmAsync() {
        CryptoService crypto = new(PasswordTextBox.Text.ToCharArray());
        using KeyHandler keyHandler = new(logger, crypto);
        var result = await keyHandler.TryGet();

        return result.Match(
            some => Option.Some(some),
            error => DisplayErrors(error),
            () => Option.None<IConnectionService>()
        );
    }

    private Option<IConnectionService> DisplayErrors(KeyHandler.KeyError failResult) {

        if(failResult == KeyHandler.KeyError.MissingKey) {
            OnMissingKey?.Invoke(this, EventArgs.Empty);
            return Option.None<IConnectionService>();
        }

        if(failResult == KeyHandler.KeyError.InvalidOrExpiredKey) {
            OnExpiredKey?.Invoke(this, EventArgs.Empty);
            return Option.None<IConnectionService>();
        }

        var errorMessage = failResult switch {
            KeyHandler.KeyError.Timeout => "The key check has timed out.",
            KeyHandler.KeyError.WrongPassword => "Wrong password.",
            _ => "The verification process has failed for an unknown reason. Please check everything is correct and retry again."
        };

        OnSendMessage?.Invoke(this, errorMessage);
        return Option.None<IConnectionService>();
    }

    private void ResetKeyButton_Click(object sender, System.Windows.RoutedEventArgs e) {
        OnChangeAPIRequest?.Invoke(this, EventArgs.Empty);
    }
}
