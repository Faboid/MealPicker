using MealPicker.Core.Services;
using MealPicker.Utils;
using System.Threading.Tasks;

namespace MealPicker.UI.WPF.Pages.Interface {
    
    /// <summary>
    /// Represents a form used to ask the data necessary to build a <see cref="IConnectionService"/>.
    /// </summary>
    internal interface IForm {

        /// <summary>
        /// Validates the inserted values and returns <see cref="IConnectionService"/> if they are valid.
        /// </summary>
        /// <returns></returns>
        public Task<Option<IConnectionService>> ConfirmAsync();

    }
}
