using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MealPickerLibrary.Files {
    public static class API_Key {

        /// <summary>
        /// Gets the directory where the application's exe has been executed in.
        /// </summary>
        /// <returns>A string with the path to the exe's folder.</returns>
        public static string GetWorkingDirectory() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Gets the path to the text file that holds the api key.
        /// </summary>
        private readonly static string txtFileWithKey = Path.Combine(GetWorkingDirectory(), "KEY.txt");

        /// <summary>
        /// Checks whether <see cref="txtFileWithKey"/> has been written.
        /// </summary>
        public static bool Check() {
            //todo - add a way to check if the key is valid

            return string.IsNullOrEmpty(Get()) is false;
        }

        /// <summary>
        /// Writes <paramref name="key"/> to <see cref="txtFileWithKey"/>.
        /// </summary>
        /// <param name="key">The API key to write.</param>
        public static void Set(string key) {
            //todo - add a way to check if the key is valid

            //todo - add some kind of cryptography
            File.WriteAllText(txtFileWithKey, key);
        }

        /// <summary>
        /// Reads <see cref="txtFileWithKey"/>.
        /// </summary>
        /// <returns>The API key written in the text file.</returns>
        public static string Get() {
            return File.ReadAllText(txtFileWithKey);
        }

    }
}
