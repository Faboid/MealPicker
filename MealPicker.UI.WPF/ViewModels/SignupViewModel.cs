using MealPicker.UI.WPF.Commands;
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

	public SignupViewModel() {
		SignupCommand = new SignupCommand();
	}

}