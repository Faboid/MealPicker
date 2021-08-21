using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MealPickerLibrary.Generic {

    /// <summary>
    /// This class is used to automatically clear a list after a certain amount of time has passed.
    /// </summary>
    /// <typeparam name="T">The object type that will be stored in the list.</typeparam>
    public class TimeoutContainer<T> {

        /// <summary>
        /// This class is used to automatically clear a list after the time has passed.
        /// </summary>
        /// <param name="milliseconds">How many milliseconds until the list should be cleared.</param>
        public TimeoutContainer(int milliseconds) {
            timer = new Timer(milliseconds);
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            Content.Clear();
        }

        /// <summary>
        /// The list that will be cleared once the time's up.
        /// </summary>
        public List<T> Content { get; private set; } = new List<T>();

        private Timer timer;

        /// <summary>
        /// Sets the content of the list and resets the timer.
        /// </summary>
        /// <param name="content">The content that should be put as the list.</param>
        public void SetContent(List<T> content) {
            Reset();
            Content = content;
            timer.Start();
        }

        /// <summary>
        /// Clears the list and stops the timer.
        /// </summary>
        public void Reset() {
            Content.Clear();
            timer.Stop();
        }

    }
}
