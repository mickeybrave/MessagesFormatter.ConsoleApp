using System.Text;
using System.Text.RegularExpressions;

namespace MessagesFormatter.ConsoleApp
{
    public class Formatter
    {
        private static Formatter _instance;
        /// <summary>
        /// Implemented here Singletone pattern just for convenience instead of calling constructore many times in tests and using 1 instance only because in this case it is optimal way of usage
        /// </summary>
        public static Formatter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Formatter();
                }
                return _instance;
            }
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Formatter()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        /// <summary>
        ///Implementation of interpolation search by using iteration approach
        /// </summary>
        public string Interpolate(string textInput, Dictionary<string, string> replacementsDicionary)
        {
            string textResult = textInput;
            int startIndex = 0;
            while (true)
            {
                int leftBracketIndex = textResult.IndexOf('[', startIndex);
                if (leftBracketIndex == -1)
                {
                    break;
                }

                // Check for escaped double square brackets
                if (leftBracketIndex > 0 && textResult[leftBracketIndex + 1] == '[')
                {
                    startIndex = leftBracketIndex + 1;
                    continue;
                }

                int rightBracketIndex = textResult.IndexOf(']', leftBracketIndex + 1);
                if (rightBracketIndex == -1)
                {
                    throw new ArgumentException("Invalid usage of brackets!");
                }

                string replacingText = textResult.Substring(leftBracketIndex + 1, rightBracketIndex - leftBracketIndex - 1);

                var cleanValue = replacingText;
                if (replacementsDicionary.ContainsKey(replacingText))
                {
                    cleanValue = replacementsDicionary[replacingText];
                }

                textResult = textResult.Substring(0, leftBracketIndex) + cleanValue + textResult.Substring(rightBracketIndex + 1);
                startIndex = leftBracketIndex + cleanValue.Length;
            }
            return textResult;
        }

        /// <summary>
        /// Implementation of interpolation search by using Regex approach
        /// </summary>
        public string InterpolateWithReplacementsRegex(string textInput, Dictionary<string, string> replacementsDicionary)
        {
            const string pattern = @"(?<!\[)\[(?:(?!\[\])\[[^\]]*\]|[^]])+\]"; // Matches any character except ] or [ within brackets
            return Regex.Replace(textInput, pattern, match =>
            {
                string key = match.Value.TrimStart('[').TrimEnd(']'); // Extract key from match (handling extra brackets)
                var cleanValue = key;// get cleaned from any additional brackets
                if (replacementsDicionary.ContainsKey(key))
                {
                    return replacementsDicionary[key];
                }
                else
                {
                    // Return the entire matched string (including brackets)
                    return $"[{cleanValue}]";
                }
            });
        }

    }
}
