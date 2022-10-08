using MealPicker.Core.Files;
using MealPicker.Core.Services;
using MealPicker.UI.WPF.Services;
using MealPicker.UI.WPF.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MealPicker.UI.WPF.Commands;

public class SignupCommand : AsyncCommandBase {

    private readonly SignupViewModel _signupViewModel;
    private readonly INotificationService _notificationService;
    private readonly IKeyHandlerFactory _keyHandlerFactory;
    private readonly NavigationService<RecipeGeneratorViewModel, IConnectionService> _navigationServiceToRecipeGeneratorVM;
    private readonly ILogger<SignupCommand>? _logger;

    public SignupCommand(SignupViewModel signupViewModel, 
                        INotificationService notificationService, IKeyHandlerFactory keyHandlerFactory, 
                        NavigationService<RecipeGeneratorViewModel, IConnectionService> navigationServiceToRecipeGeneratorVM, 
                        ILoggerFactory? loggerFactory = null) {
        _signupViewModel = signupViewModel;
        _notificationService = notificationService;
        _keyHandlerFactory = keyHandlerFactory;
        _navigationServiceToRecipeGeneratorVM = navigationServiceToRecipeGeneratorVM;
        _logger = loggerFactory?.CreateLogger<SignupCommand>();
    }

    protected override async Task ExecuteAsync(object? parameter) {

        if(_signupViewModel.Password != _signupViewModel.RepeatPassword) {
            _notificationService.Send("The passwords must be equal.");
            return;
        }

        _logger?.LogInformation("Beginning setting up a new password and API key.");
        var keyHandler = _keyHandlerFactory.CreateKeyHandler(_signupViewModel.Password.ToCharArray());
        var result = await keyHandler.TrySet(_signupViewModel.ApiKey);


        if(result.Result() is not Utils.Options.OptionResult.Some) {
            var message = result.Match(
                some => throw new NotSupportedException(),
                DisplayErrors,
                () => "There has been an unknown error."
            );

            _logger?.LogInformation("There has been an error when setting up a new key and password: {ErrorCode}", result.OrError(0));
            _notificationService.Send(message);
            return;

        } 

        var connectionService = result.Or(null!);
        if(connectionService is not null) {
            _logger?.LogInformation("The new key and passwords have been set up successfully.");
            _navigationServiceToRecipeGeneratorVM.Navigate(connectionService, true);
        } else {
            _logger?.LogError("There has been an error when setting up a new key and password: The option returned Some, but was null.");
            _notificationService.Send("There has been an error loading the recipe generator.");
        }

    }

    /// <summary>
    /// Converts <paramref name="failResult"/> to an user-friendly error message.
    /// </summary>
    /// <param name="failResult"></param>
    /// <returns></returns>
    private string DisplayErrors(KeyHandler.KeyError failResult) {
        return failResult switch {
            KeyHandler.KeyError.Timeout => "The key check has timed out.",
            KeyHandler.KeyError.InvalidOrExpiredKey => "The given key is invalid.",
            _ => "The verification process has failed for an unknown reason. Please check everything is correct and retry again."
        };
    }

}