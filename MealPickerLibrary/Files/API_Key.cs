using MealPickerLibrary.Queries;
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
        public readonly static string txtFileWithKey = Path.Combine(GetWorkingDirectory(), "KEY.txt");

        /// <summary>
        /// Checks whether <see cref="txtFileWithKey"/> has been written.
        /// </summary>
        /// <param name="checkIfCorrect">Whether to call the API and confirm the functioning of the key.</param>
        /// <returns><see langword="True"/> if the saved key exists and, if <paramref name="checkIfCorrect"/> is true, if it functions correctly.</returns>
        public static async Task<bool> Check(bool checkIfCorrect) {

            if(Check() is false) {
                //if the file has no key, return false
                return false;

            } else if(checkIfCorrect) {
                //calls the api to check if it's correct
                return await Connection.TestKey(Get());

            } else {
                return true;
            }
        }

        /// <summary>
        /// Checks whether <see cref="txtFileWithKey"/> has been written.
        /// </summary>
        public static bool Check() {
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
            if(File.Exists(txtFileWithKey) is false) {
                return null;
            }

            return File.ReadAllText(txtFileWithKey);
        }

    }
}
