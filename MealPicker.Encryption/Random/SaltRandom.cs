namespace MealPicker.Encryption.Random {

    /// <summary>
    /// Provides a simple way of getting a 'random' value between minimum and maximum values.
    /// <br/><br/>
    /// Important: the algorithm behind this is extremely simple. Its purpose is to be consistent, not hard to guess.
    /// </summary>
    internal class SaltRandom {

        readonly int seed;
        IntRange current;

        /// <summary>
        /// Initializes <see cref="SaltRandom"/> with the given <paramref name="seed"/>.
        /// </summary>
        /// <param name="seed"></param>
        public SaltRandom(int seed) : this(34, 64, seed) { }

        /// <summary>
        /// Initializes <see cref="SaltRandom"/> while making sure all the generated values stay from <paramref name="min"/> to <paramref name="max"/>.
        /// </summary>
        /// <param name="min">The minimum value that will be returned by <see cref="Next"/>.</param>
        /// <param name="max">The maximum value that will be returned by <see cref="Next"/>.</param>
        /// <param name="seed">Used to generate the values.</param>
        public SaltRandom(int min, int max, int seed) {
            this.seed = seed;
            current = new IntRange(min, max, seed);
        }

        /// <summary>
        /// Gets the next value. Do not use this when randomness is important—only when you require consistent results.
        /// </summary>
        /// <returns></returns>
        public int Next() {
            current += seed;
            return current;
        }

    }
}
