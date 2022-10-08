using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MealPicker.UI.WPF.Components;
/// <summary>
/// Interaction logic for FormWrapper.xaml
/// </summary>
[ContentProperty("Items")]
public partial class FormWrapper : UserControl {

    public ICommand ComfirmFormCommand {
        get { return (ICommand)GetValue(ComfirmFormCommandProperty); }
        set { SetValue(ComfirmFormCommandProperty, value); }
    }

    public static readonly DependencyProperty ComfirmFormCommandProperty =
        DependencyProperty.Register("ComfirmFormCommand", typeof(ICommand), typeof(FormWrapper), new PropertyMetadata(null));

    public IEnumerable<FormItem> FormItems {
        get { return (IEnumerable<FormItem>)GetValue(FormItemsProperty); }
        set { SetValue(FormItemsProperty, value); }
    }

    public static readonly DependencyProperty FormItemsProperty = ItemsControl.ItemsSourceProperty.AddOwner(typeof(FormWrapper), new PropertyMetadata(null));

    public ItemCollection Items {
        get => _itemsControl.Items;
    }

    public FormWrapper() {
        InitializeComponent();
    }

}
