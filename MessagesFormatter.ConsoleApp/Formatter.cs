﻿using System.Text;
using System.Text.RegularExpressions;

namespace MessagesFormatter.ConsoleApp
{
    public class Formatter
    {
        private static Formatter _instance;
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
        public string Interpolate(string text, Dictionary<string, string> replacements)
        {
            string result = text;
            int startIndex = 0;
            while (true)
            {
                int openingBracketIndex = result.IndexOf('[', startIndex);
                if (openingBracketIndex == -1)
                {
                    break;
                }

                // Check for escaped double square brackets
                if (openingBracketIndex > 0 && result[openingBracketIndex + 1] == '[')
                {
                    startIndex = openingBracketIndex + 1;
                    continue;
                }

                int closingBracketIndex = result.IndexOf(']', openingBracketIndex + 1);
                if (closingBracketIndex == -1)
                {
                    throw new ArgumentException("Invalid format: unmatched opening bracket");
                }

                string key = result.Substring(openingBracketIndex + 1, closingBracketIndex - openingBracketIndex - 1);

                var cleanValue = key;
                if (replacements.ContainsKey(key))
                {
                    cleanValue = replacements[key];
                }

                result = result.Substring(0, openingBracketIndex) + cleanValue + result.Substring(closingBracketIndex + 1);
                startIndex = openingBracketIndex + cleanValue.Length;
            }
            return result;
        }


        public string InterpolateWithReplacementsRegex(string text, Dictionary<string, string> replacements)
        {
            string pattern = @"(?<!\[)\[(?:(?!\[\])\[[^\]]*\]|[^]])+\]"; // Matches any character except ] within brackets, excluding nested double brackets
            return Regex.Replace(text, pattern, match =>
            {
                string key = match.Value.TrimStart('[').TrimEnd(']'); // Extract key from match (handling extra brackets)
                var cleanValue = key;// get cleaned from any additional brackets
                if (replacements.ContainsKey(key))
                {
                    return replacements[key];
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