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

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(FormItem), 
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, null, false, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));

    public string Label {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }

    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register("Label", typeof(string), typeof(FormItem), new PropertyMetadata(string.Empty));

    public FormItem() {
        InitializeComponent();
    }
}
