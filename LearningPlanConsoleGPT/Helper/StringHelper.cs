using System.Text.RegularExpressions;

namespace LearningPlanConsoleGPT.Helper
{
    public class StringHelper
    {
        public static string ReplaceSpecialChars(string input)
        {
            // Use regular expression to replace special characters with underscores
            string pattern = @"[^\w\d]"; // Matches any non-word, non-digit character
            string replacement = "_";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(input, replacement);
            return result;
        }
    }
}
