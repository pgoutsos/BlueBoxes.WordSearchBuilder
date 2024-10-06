using System.Text.RegularExpressions;

namespace BlueBoxes.WordSearchBuilder.Formatters;

public class SpanishWordFormatter : IWordFormatter
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
        for (var i = 0; i < wordArray.Length; i++)
        {
            wordArray[i] = wordArray[i] switch
            {
                'á' => 'a',
                'é' => 'e',
                'í' => 'i',
                'ó' => 'o',
                'ú' => 'u',
                _ => wordArray[i]
            };
        }

        inputWord = new string(wordArray);

        //Console.WriteLine(Regex.Replace(inputWord.ToUpperInvariant(), "[^A-ZÑ0-9]", "")); 
        return Regex.Replace(inputWord.ToUpperInvariant(), "[^A-ZÑ0-9]", "");
    }
}
