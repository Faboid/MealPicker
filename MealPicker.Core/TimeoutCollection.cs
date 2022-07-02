using MealPicker.Utils;
using System.Diagnostics.CodeAnalysis;

namespace MealPicker.Core; 

/// <summary>
/// A list that will delete all its contents after a given time.
/// </summary>
/// <typeparam name="T"></typeparam>
internal class TimeoutCollection<T> : IDisposable {

    /// <summary>
    /// Creates an instance of <see cref="TimeoutCollection{T}"/>.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="recipes"></param>
    /// <param name="timeout"></param>
    public TimeoutCollection(ILogger logger, IList<T> recipes, TimeSpan timeout) {
        this.timeout = timeout;
        this.logger = logger;
        Renew(recipes);
    }

    private readonly ILogger logger;
    readonly TimeSpan timeout;
    private Timer timer;
    private IList<T> recipes;

    public int Count => recipes.Count;

    private void Reset(object? unused) {
        recipes.Clear();
        logger.LogInfo("The TimeoutCollection's timer has expired and the recipes have been cleared.");
    }

    /// <summary>
    /// Clears all remaining values, restarts the timer, and stores the given <paramref name="recipes"/>.
    /// </summary>
    /// <param name="recipes"></param>
    [MemberNotNull(nameof(timer), nameof(recipes))]
    public void Renew(IList<T> recipes) {
        this.recipes?.Clear();
        timer?.Change(timeout, TimeSpan.Zero);

        timer ??= new Timer(Reset, null, timeout, TimeSpan.Zero);
        this.recipes = recipes;
    }

    /// <summary>
    /// Returns the next recipe, then deletes it from this collection.
    /// </summary>
    /// <returns>An option with <typeparamref name="T"/> if there is at least one value; otherwise, <see cref="Option.None{TValue}"/>.</returns>
    public Option<T> Next() {

        var recipe = recipes.FirstOrDefault();
        if(recipe == null) {
            return Option.None<T>();
        }

        recipes.Remove(recipe);
        return recipe;
    }

    public void Dispose() {
        recipes.Clear();
        timer.Dispose();
    }
}
