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

    public string ConfirmButtonText {
        get { return (string)GetValue(ConfirmButtonTextProperty); }
        set { SetValue(ConfirmButtonTextProperty, value); }
    }

    public static readonly DependencyProperty ConfirmButtonTextProperty =
        DependencyProperty.Register(nameof(ConfirmButtonText), typeof(string), typeof(FormWrapper), new PropertyMetadata("Comfirm"));

    public ICommand ConfirmFormCommand {
        get { return (ICommand)GetValue(ConfirmFormCommandProperty); }
        set { SetValue(ConfirmFormCommandProperty, value); }
    }

    public static readonly DependencyProperty ConfirmFormCommandProperty =
        DependencyProperty.Register(nameof(ConfirmFormCommand), typeof(ICommand), typeof(FormWrapper), new PropertyMetadata(null));

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
