using MealPickerLibrary.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MealPickerLibrary.Queries {

    /// <summary>
    /// A class that handles making requests to the spoonacular API.
    /// </summary>
    public static class Connection {

        private readonly static HttpClient client;
        private static string apiKey { get => API_Key.Get(); }
        private static string apiKeySign = "apiKey";

        static Connection() {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.spoonacular.com/recipes/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<bool> TestKey(string key) {
            try {
                await CallAPIAsync<ListRecipesResult>($"random?{apiKeySign}={key}&number={1}&limitLicense=true");
            } catch (HttpRequestException ex) {
                if(ex.Message.Contains("Unauthorized")) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Calls an API by using the given path. Throws an exception if the request fails.
        /// </summary>
        /// <typeparam name="T">The object that will be returned after converting the calls' response values.</typeparam>
        /// <param name="path">The path to call the API.</param>
        /// <returns>An instantiated object filled with the properties gained from the API.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public static async Task<T> CallAPIAsync<T>(string path) where T: class {
            T product = null;
            using(HttpResponseMessage response = await client.GetAsync(path)) {
                
                response.EnsureSuccessStatusCode();
                
                product = await response.Content.ReadAsAsync<T>();
            }
            return product;
        }

        /// <summary>
        /// Calls the spoonacular API to get a list of recipes. Throws an exception if the call fails.
        /// </summary>
        /// <param name="number">The amount of recipes to request.</param>
        /// <returns>A <see cref="ListRecipesResult"/> that contains a list with all the recipes.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public static async Task<ListRecipesResult> GetRandomRecipesAsync(int number) {
            return await CallAPIAsync<ListRecipesResult>($"random?{apiKeySign}={apiKey}&number={number}&limitLicense=true");
        }

        /// <summary>
        /// Calls the spoonacular API to get info regarding a specific recipe.
        /// </summary>
        /// <param name="ID">The ID of the recipe.</param>
        /// <returns>A <see cref="RecipeModel"/> containing all the info of the recipe.</returns>
        public static async Task<RecipeModel> GetRecipeByIDAsync(string ID) {
            return await CallAPIAsync<RecipeModel>(BuildRequestRecipeByID(ID));
        }

        private static string BuildRequestRecipeByID(string ID) {
            return $"{ID}/information?{apiKeySign}={apiKey}&includeNutrition=false";
        }

    }
}
