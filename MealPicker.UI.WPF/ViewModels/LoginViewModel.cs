using MealPicker.UI.WPF.Commands;
using MealPicker.UI.WPF.Services;
using MealPicker.UI.WPF.Stores;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace MealPicker.UI.WPF.ViewModels;

public class LoginViewModel : ViewModelBase {

	private string _password = string.Empty;
	public string Password {
		get { return _password; }
		set { SetAndRaise(nameof(Password), ref _password, value); }
	}

	public ICommand ChangeAPIKeyCommand { get; }
    public ICommand ConfirmCommand { get; }

	public LoginViewModel(NavigationService<SignupViewModel> navigateToSignupViewModel, 
						CryptoContainerStore cryptoContainerStore, INotificationService notificationService, 
						NavigationService<RecipeGeneratorViewModel> navigationServiceToRecipeGeneratorVM, 
						ILoggerFactory? loggerFactory = null) {
		ConfirmCommand = new LoginCommand(this, cryptoContainerStore, notificationService, navigationServiceToRecipeGeneratorVM, loggerFactory);
		ChangeAPIKeyCommand = new NavigateCommand<SignupViewModel>(true, navigateToSignupViewModel);
	}

}