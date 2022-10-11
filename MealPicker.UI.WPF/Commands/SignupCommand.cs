using MealPicker.UI.WPF.Services;
using MealPicker.UI.WPF.Stores;
using MealPicker.UI.WPF.ViewModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MealPicker.UI.WPF.Commands;

public class SignupCommand : AsyncCommandBase {

    private readonly SignupViewModel _signupViewModel;
    private readonly INotificationService _notificationService;
    private readonly CryptoContainerStore _cryptoContainerStore;
    private readonly NavigationService<RecipeGeneratorViewModel> _navigationServiceToRecipeGeneratorVM;
    private readonly ILogger<SignupCommand>? _logger;

    public SignupCommand(SignupViewModel signupViewModel,
                        INotificationService notificationService, CryptoContainerStore cryptoContainerStore,
                        NavigationService<RecipeGeneratorViewModel> navigationServiceToRecipeGeneratorVM,
                        ILoggerFactory? loggerFactory = null) {
        _signupViewModel = signupViewModel;
        _cryptoContainerStore = cryptoContainerStore;
        _notificationService = notificationService;
        _navigationServiceToRecipeGeneratorVM = navigationServiceToRecipeGeneratorVM;
        _logger = loggerFactory?.CreateLogger<SignupCommand>();
    }

    protected override async Task ExecuteAsync(object? parameter) {

        if(_signupViewModel.Password != _signupViewModel.RepeatPassword) {
            _notificationService.Send("The passwords must be equal.");
            return;
        }

        _logger?.LogInformation("Beginning setting up a new password and API key.");
        var result = await _cryptoContainerStore.SignupConnection(_signupViewModel.Password.ToCharArray(), _signupViewModel.ApiKey);

        if(result is CryptoContainerStore.SignupResult.Success) {
            _logger?.LogInformation("The new key and passwords have been set up successfully.");
            _navigationServiceToRecipeGeneratorVM.Navigate(true);
            return;
        }

        _logger?.LogInformation("There has been an error when setting up a new key and password: {ErrorCode}", result);
        var message = result switch {
            CryptoContainerStore.SignupResult.InvalidKey => "The given key is invalid.",
            CryptoContainerStore.SignupResult.Timeout => "The key check has timed out.",
            _ => "The verification process has failed for an unknown reason. Please check everything is correct and retry again.",
        };
        
        _notificationService.Send(message);

    }

}