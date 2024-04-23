using System.ComponentModel;

namespace MorseCrypter.Core;

public class Base36ToMorseTranslator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public Base36ToMorseTranslator()
    {

    }

    public string ConvertBase36ToMorse(Dictionary<string, string> transSet, string base36Text)
    {
        var morseText = string.Empty;

        foreach (var c in base36Text)
        {
            transSet.TryGetValue(c.ToString(), out var morseValue);
            //Add space for inbvetween characters
            //Add | (pipe) for inbetween words
            morseText += morseValue;
        }

        return morseText;
    }
}

public class MorseToBase36Translator()
{

}