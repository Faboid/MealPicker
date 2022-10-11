using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MealPicker.UI.WPF.Views;
/// <summary>
/// Interaction logic for RecipeGeneratorView.xaml
/// </summary>
public partial class RecipeGeneratorView : UserControl {
    
    public RecipeGeneratorView() {
        InitializeComponent();
    }

    public ICommand LoadedCommand {
        get { return (ICommand)GetValue(LoadedCommandProperty); }
        set { SetValue(LoadedCommandProperty, value); }
    }

    // Using a DependencyProperty as the backing store for LoadedCommand.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LoadedCommandProperty =
        DependencyProperty.Register("LoadedCommand", typeof(ICommand), typeof(RecipeGeneratorView), new PropertyMetadata(null));

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
        LoadedCommand?.Execute(null);
    }
}
