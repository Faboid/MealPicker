﻿using MealPicker.Utils;
using System.Diagnostics.CodeAnalysis;

namespace MealPicker.Core; 

internal class TimeoutCollection<T> : IDisposable {

    public TimeoutCollection(List<T> recipes, TimeSpan timeout) {
        this.timeout = timeout;
        Renew(recipes);
    }

    readonly TimeSpan timeout;
    private Timer timer;
    private List<T> recipes;

    public int Count => recipes.Count;

    private void Reset(object? unused) {
        recipes.Clear();
    }

    [MemberNotNull(nameof(timer), nameof(recipes))]
    public void Renew(List<T> recipes) {
        this.recipes?.Clear();
        timer?.Change(timeout, TimeSpan.Zero);

        timer ??= new Timer(Reset, null, timeout, TimeSpan.Zero);
        this.recipes = recipes;
    }

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
