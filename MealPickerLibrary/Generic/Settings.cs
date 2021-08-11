using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MealPickerLibrary.Generic {
    public static class Settings {

        public static void SetCurrentThreadToEnglish() {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Sets the current thread to english only if it's used during a debug session.
        /// </summary>
        public static void IFDEBUGSetCurrentThreadToEnglish() {
#if DEBUG
            SetCurrentThreadToEnglish();
#endif
        }

    }
}
