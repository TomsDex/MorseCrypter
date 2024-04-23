using System.Reflection;

namespace MorseCrypter.Core;

public class Base36ToMorseTranslator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public Base36ToMorseTranslator()
    {

    }

    public string ConvertBase36ToMorse(FileReader fileReader, string base36Text)
    {
        var morseText = String.Empty;

        foreach (var c in base36Text)
        {
            fileReader.CharacterSets[0].TryGetValue(c.ToString(), out var morseValue);
            morseText += morseValue;
        }

        return morseText;
    }
}

public class MorseToBase36Translator()
{

}