using MealPicker.UI.WPF.Commands;
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

	public LoginViewModel(Services.NavigationService<SignupViewModel> navigateToSignupViewModel) {
		ConfirmCommand = new LoginCommand();
		ChangeAPIKeyCommand = new NavigateCommand<SignupViewModel>(true, navigateToSignupViewModel);
	}

}