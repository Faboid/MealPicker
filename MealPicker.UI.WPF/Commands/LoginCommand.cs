using MealPicker.Core.Files;
using MealPicker.Core.Services;
using MealPicker.UI.WPF.Services;
using MealPicker.UI.WPF.ViewModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MealPicker.UI.WPF.Commands;

public class LoginCommand : AsyncCommandBase {

    private readonly LoginViewModel _loginViewModel;
    private readonly IKeyHandlerFactory _keyHandlerFactory;
    private readonly INotificationService _notificationService;
    private readonly NavigationService<RecipeGeneratorViewModel, IConnectionService> _navigationServiceToRecipeGeneratorVM;
    private readonly ILogger<LoginCommand>? _logger;

    public LoginCommand(LoginViewModel loginViewModel, 
                        IKeyHandlerFactory keyHandlerFactory, INotificationService notificationService, 
                        NavigationService<RecipeGeneratorViewModel, IConnectionService> navigationServiceToRecipeGeneratorVM, 
                        ILoggerFactory? loggerFactory) {
        _loginViewModel = loginViewModel;
        _keyHandlerFactory = keyHandlerFactory;
        _notificationService = notificationService;
        _navigationServiceToRecipeGeneratorVM = navigationServiceToRecipeGeneratorVM;
        _logger = loggerFactory?.CreateLogger<LoginCommand>();
    }

    protected override async Task ExecuteAsync(object? parameter) {

        var keyHandler = _keyHandlerFactory.CreateKeyHandler(_loginViewModel.Password.ToCharArray());
        var result = await keyHandler.TryGet();

        var conn = result.Or(default!);
        if(conn != null) {
            _navigationServiceToRecipeGeneratorVM.Navigate(conn, true);
        }

        _logger?.LogInformation("There has been an error logging in: {ErrorCode}", result.OrError(0));
        var message = result.Match(
            some => "There has been an unknown error.",
            DisplayErrors,
            () => "There has been an unknown error."
        );

        _notificationService.Send(message);

    }

    /// <summary>
    /// Converts <paramref name="failResult"/> to an user-friendly error message.
    /// </summary>
    /// <param name="failResult"></param>
    /// <returns></returns>
    private string DisplayErrors(KeyHandler.KeyError failResult) {
        return failResult switch {
            KeyHandler.KeyError.Timeout => "The key check has timed out.",
            KeyHandler.KeyError.WrongPassword => "Wrong password.",
            _ => "The verification process has failed for an unknown reason. Please check everything is correct and retry again."
        };
    }

}