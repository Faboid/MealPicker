using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MealPickerLibrary {
    public static class Connection {

        private static HttpClient client;
        private static string apiKey;

        static Connection() {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.spoonacular.com/recipes/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        static async Task<T> GetRecipeAsync<T>(string path) where T: class {
            T product = null;
            using(HttpResponseMessage response = await client.GetAsync(path)) {
                if(response.IsSuccessStatusCode) {
                    product = await response.Content.ReadAsAsync<T>();
                }
            }
            return product;
        }

        static string BuildRequestRecipeByID(string ID) {
            return $"{ID}/information?apiKey={apiKey}&includeNutrition=false";
        }

    }
}
