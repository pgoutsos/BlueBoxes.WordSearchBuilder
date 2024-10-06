using System.Text.RegularExpressions;

namespace BlueBoxes.WordSearchBuilder.Formatters;
public class EnglishWordFormatter : IWordFormatter
{
    /// <summary>
    /// Formats the input word to fit into the grid removing spaces, hyphens etc.
    /// </summary>
    /// <param name="inputWord"></param>
    /// <param name="includeSpanish"></param>
    /// <returns></returns>
    public string FormatWord(string inputWord)
    {
        var wordArray = inputWord.ToCharArray();
        return Regex.Replace(inputWord.ToUpperInvariant(), "[^A-Z0-9]", "");
    }
}
