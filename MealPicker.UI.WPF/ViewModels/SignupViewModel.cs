using MealPicker.Core.Files;
using MealPicker.Core.Services;
using MealPicker.UI.WPF.Commands;
using MealPicker.UI.WPF.Services;
using MealPicker.UI.WPF.Stores;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace MealPicker.UI.WPF.ViewModels;

public class SignupViewModel : ViewModelBase {

	private string _apyKey = "";
	public string ApiKey {
		get { return _apyKey; }
		set { SetAndRaise(nameof(ApiKey), ref _apyKey, value); }
	}

	private string _password = "";
	public string Password {
		get { return _password; }
		set { SetAndRaise(nameof(Password), ref _password, value); }
	}

	private string _repeatPassword = "";
	public string RepeatPassword {
		get { return _repeatPassword; }
		set { SetAndRaise(nameof(RepeatPassword), ref _repeatPassword, value); }
	}

	public ICommand SignupCommand { get; }

	public SignupViewModel(INotificationService notificationService, CryptoContainerStore cryptoContainerStore, 
							NavigationService<RecipeGeneratorViewModel> navigationServiceToRecipeGeneratorVM, 
							ILoggerFactory? loggerFactory = null) {
		SignupCommand = new SignupCommand(this, notificationService, cryptoContainerStore, navigationServiceToRecipeGeneratorVM, loggerFactory);
	}

}