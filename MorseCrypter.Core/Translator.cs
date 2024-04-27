namespace MorseCrypter.Core;

/// <summary>
/// Contains the conversion to/from Morse code logic.
/// </summary>
public static class Translator
{
    /// <summary>
    /// Converts an encoded string to a Morse code string.
    /// </summary>
    /// <param name="input">The text to be translated to Morse code.</param>
    /// <param name="transSet">The translation set.</param>
    /// <returns>The encoded string after it has been translated into Morse code.</returns>
    public static string ConvertEncodedToMorse(string input, Dictionary<string, string> transSet)
    {
        var morseText = string.Empty;

        foreach (var c in input)
        {
            transSet.TryGetValue(c.ToString(), out var morseValue);
            morseText += morseValue;
            morseText += " ";
        }
        return morseText;
    }


    /// <summary>
    /// Converts a Morse code string to an encoded string.
    /// </summary>
    /// <param name="morseText">The Morse code to be translated into text.</param>
    /// <param name="transSet">The translation set.</param>
    /// <returns>The encoded string after it has been translated from Morse code.</returns>
    public static string ConvertMorseToEncoded(string morseText, Dictionary<string, string> transSet)
    {
        var translatedText = string.Empty;

        //Split the Morse text into individual characters.
        var morseChars = morseText.Split(" ");

        //Translate each Morse character to a Base36 character according to the translation set.
        return morseChars.Select(morseChar => transSet
            .FirstOrDefault(x => x.Value == morseChar).Key)
            .Aggregate(translatedText, (current, base36Key) => current + base36Key);
    }
}