

using MessagesFormatter.ConsoleApp;

namespace MessagesFormatter.ConsoleApp.Tests
{
    public class FormatterTests
    {

        [Fact]
        public void Formatter_Interpolate_substituted_into_square_brackets_tests()
        {
            Assert.Equal("Hello Jim", Formatter.Instance.Interpolate("Hello [name]", new Dictionary<string, string> { { "name", "Jim" } }));
        }

        [Fact]
        public void Formatter_Interpolate_substituted_escape_square_brackets_by_doubling_tests()
        {
            var result = Formatter.Instance.Interpolate("Hello [name] [[author]]", new Dictionary<string, string> { { "name", "Jim" } });
            // escape the square brackets by doubling them:
            Assert.Equal("Hello Jim [author]", result);
        }

        [Fact]
        public void Formatter_Interpolate_substituted_into_square_brackets_longer_text_tests()
        {

            string message = "Hello, [name]! Your order of [quantity] [item] has shipped.";
            Dictionary<string, string> values = new Dictionary<string, string>()
                            {
                              { "name", "Michael Braverman" },
                              { "quantity", "2" },
                              { "item", "books" }

                            };

            var result = Formatter.Instance.Interpolate(message, values);
            Assert.Equal("Hello, Michael Braverman! Your order of 2 books has shipped.", result);
        }


        [Fact]
        public void Formatter_Interpolate_substituted_into_square_brackets_Regex_tests()
        {
            Assert.Equal("Hello Jim", Formatter.Instance.InterpolateWithReplacementsRegex("Hello [name]", new Dictionary<string, string> { { "name", "Jim" } }));
        }

        [Fact]
        public void Formatter_Interpolate_substituted_escape_square_brackets_by_doubling_Regex_tests()
        {
            var result = Formatter.Instance.InterpolateWithReplacementsRegex("Hello [name] [[author]]", new Dictionary<string, string> { { "name", "Jim" } });
            // escape the square brackets by doubling them:
            Assert.Equal("Hello Jim [author]", result);
        }

        [Fact]
        public void Formatter_Interpolate_substituted_into_square_brackets_longer_text_Regex_tests()
        {

            string message = "Hello, [name]! Your order of [quantity] [item] has shipped.";
            Dictionary<string, string> values = new Dictionary<string, string>()
                            {
                              { "name", "Michael Braverman" },
                              { "quantity", "2" },
                              { "item", "books" }

                            };

            var result = Formatter.Instance.InterpolateWithReplacementsRegex(message, values);
            Assert.Equal("Hello, Michael Braverman! Your order of 2 books has shipped.", result);
        }
    }
}