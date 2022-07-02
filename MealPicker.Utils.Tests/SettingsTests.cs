using MealPicker.Utils.Options;
using Xunit;

namespace MealPicker.Utils.Tests {
    public class SettingsTests : IDisposable {

        readonly static string path = PathBuilder.SettingsPath;
        readonly Settings settings = new(path);

        [Theory]
        [InlineData("one", "isOne")]
        [InlineData("one", "here")] //checking if it overwrites it correctly
        [InlineData("three", "where")]
        public void CreateAndGet(string key, string value) {

            //act
            settings.Set(key, value);

            //assert
            Assert.Equal(value, settings.Get(key).Or("missing"));

        }

        [Fact]
        public void ReturnsNoneIfKeyDoesNotExist() {

            //arrange
            string key = "someNotExistingKey";

            //act
            var result = settings.Get(key);

            //assert
            Assert.Equal(OptionResult.None, result.Result());

        }

        public void Dispose() {
            File.Delete(path);
            GC.SuppressFinalize(this);
        }
    }
}
