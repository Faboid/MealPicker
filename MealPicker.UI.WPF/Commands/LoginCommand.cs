using MealPicker.UI.WPF.Services;
using MealPicker.UI.WPF.Stores;
using MealPicker.UI.WPF.ViewModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static MealPicker.UI.WPF.Stores.CryptoContainerStore;

namespace MealPicker.UI.WPF.Commands;

public class LoginCommand : AsyncCommandBase {

    private readonly LoginViewModel _loginViewModel;
    private readonly CryptoContainerStore _cryptoContainerStore;
    private readonly INotificationService _notificationService;
    private readonly NavigationService<RecipeGeneratorViewModel> _navigationServiceToRecipeGeneratorVM;
    private readonly ILogger<LoginCommand>? _logger;

    public LoginCommand(LoginViewModel loginViewModel,
                        CryptoContainerStore cryptoContainerStore, INotificationService notificationService,
                        NavigationService<RecipeGeneratorViewModel> navigationServiceToRecipeGeneratorVM,
                        ILoggerFactory? loggerFactory = null) {
        _loginViewModel = loginViewModel;
        _notificationService = notificationService;
        _navigationServiceToRecipeGeneratorVM = navigationServiceToRecipeGeneratorVM;
        _logger = loggerFactory?.CreateLogger<LoginCommand>();
        _cryptoContainerStore = cryptoContainerStore;
    }

    protected override async Task ExecuteAsync(object? parameter) {

        var result = await _cryptoContainerStore.LoginConnection(_loginViewModel.Password.ToCharArray());

        if(result is LoginResult.Success) {
            _navigationServiceToRecipeGeneratorVM.Navigate(true);
            return;
        }

        _logger?.LogInformation("There has been an error logging in: {ErrorCode}", result);
        
        var message = result switch {
            LoginResult.Timeout => "The key check has timed out.",
            LoginResult.ExpiredKey => "The key you registered previously has expired.",
            LoginResult.WrongPassword => "Wrong password.",
            _ => "The verification process has failed for an unknown reason. Please check everything is correct and retry again.",
        };

        _notificationService.Send(message);

    }

}