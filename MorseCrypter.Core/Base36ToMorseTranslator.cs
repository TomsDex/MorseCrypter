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
            switch (c)
            {
                case ' ':
                    //Add a pipe for in between words.
                    morseText += "|";
                    break;
                default:
                    transSet.TryGetValue(c.ToString(), out var morseValue);
                    morseText += morseValue;
                    break;
            }
            
            //Add two spaces for in between characters.
            morseText += "  ";
        }

        return morseText;
    }
}

public class MorseToBase36Translator()
{

}