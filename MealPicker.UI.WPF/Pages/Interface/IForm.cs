using MealPicker.Core.Services;
using MealPicker.Utils;
using System.Threading.Tasks;

namespace MealPicker.UI.WPF.Pages.Interface {
    internal interface IForm {

        public Task<Option<IConnectionService>> ConfirmAsync();

    }
}
