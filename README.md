# MealPicker

An application to choose a random recipe.

Requests random recipes from the [Spoonacular API](https://spoonacular.com/food-api) using a user-provided key and displays them in a user-friendly format. As it uses WPF, it currently only works on Windows. There are plans to add cross-platform support with ASP.NET.

Specifics:
 - A color scheme has been used and applied over the WPF UI(the default is dark theme). The theme can be changed during runtime and will be remembered throughout the sessions. As the current theme is stored in a text file, it is possible to edit it manually.
 - An encrypted version of the API key gets stored locally so that a custom password can be used instead.
 - Exceptional situations are stored in a Log.txt file in the exe's folder. Said log file will keep the information up to the last 10 sessions, and deletes everything before to avoid bloat.
 - Uses events to send messages up the chain, from pages to the main window, so that they can be displayed in a uniform manner without using a message box.
