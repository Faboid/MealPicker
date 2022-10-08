using System.Windows;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.Components;
/// <summary>
/// Interaction logic for FormItem.xaml
/// </summary>
public partial class FormItem : UserControl {

    public string Text {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(FormItem), new PropertyMetadata(string.Empty));

    public string Label {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register("Label", typeof(string), typeof(FormItem), new PropertyMetadata(string.Empty));

    public FormItem() {
        InitializeComponent();
    }
}
