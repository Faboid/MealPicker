using MealPickerLibrary.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MealPickerLibrary.Queries {
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

        public static async Task<T> CallAPIAsync<T>(string path) where T: class {
            T product = null;
            using(HttpResponseMessage response = await client.GetAsync(path)) {
                if(response.IsSuccessStatusCode) {
                    product = await response.Content.ReadAsAsync<T>();
                }
            }
            return product;
        }

        public static async Task<ListRecipesResult> GetRandomRecipesAsync(int number) {
            return await CallAPIAsync<ListRecipesResult>($"random?{apiKeySign}={apiKey}&number={number}&limitLicense=true");
        }

        public static async Task<RecipeModel> GetRecipeByIDAsync(string ID) {
            return await CallAPIAsync<RecipeModel>(BuildRequestRecipeByID(ID));
        }

        static string BuildRequestRecipeByID(string ID) {
            return $"{ID}/information?{apiKeySign}={apiKey}&includeNutrition=false";
        }

    }
}
