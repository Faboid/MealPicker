# MealPicker

MealPicker is an application to choose a random recipe.

Requests random recipes from the [Spoonacular API](https://spoonacular.com/food-api) using a user-provided key and displays them in a user-friendly format. As it uses WPF, it currently only works on Windows.

## Features

- A color scheme has been used and applied over the WPF UI (the default is dark theme). The theme can be changed during runtime and will be remembered throughout the sessions.
- An encrypted version of the API key gets stored locally so that a custom password can be used instead.
- Exceptional situations are stored in a Log.txt file in the exe's folder. Said log file will keep the information up to the last 10 sessions and deletes everything before to avoid bloat.

## Installation and Usage

1. Clone or download the repository.
2. Build the solution in Visual Studio.
3. Launch the MealPicker application.
4. Provide your Spoonacular API key to access random recipes.
5. Press the "random" button.
6. Enjoy!

# Terms of service for the use of this application:
Mealpicker stores in a local file an encrypted version of your API key by using the password you choose. You are responsible for choosing a safe, long password, and for regularly updating the API key.

IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
