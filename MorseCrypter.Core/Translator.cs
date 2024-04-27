namespace MorseCrypter.Core;

public static class Base36ToMorseTranslator
{
    public static string ConvertBase36ToMorse(string input, Dictionary<string, string> transSet)
    {
        var morseText = string.Empty;

        foreach (var c in input)
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

    public static string ConvertMorseToBase36(Dictionary<string, string> transSet, string morseText)
    {
        var base36Text = string.Empty;

        foreach (var c in morseText)
        {
            switch (c)
            {
                case ' ':
                    //If the space after c is also a space, ignore it (this is a character separator).
                    if (morseText.IndexOf(c) + 1 < morseText.Length && 
                        morseText[morseText.IndexOf(c) + 1] == ' ')
                    {
                        // Ignore the second space
                    }
                    break;
                case '|':
                    //Add a pipe for in between words.
                    base36Text += " ";
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

public class MorseToBase36Translator
{
    public string ConvertMorseToBase36(Dictionary<string, string> transSet, string morseText)
    {
        //TODO: Implement this method.
        //var base36Text = string.Empty;

        //foreach (var c in base36Text)
        //{
        //    switch (c)
        //    {
        //        case ' ':
        //            //Add a pipe for in between words.
        //            morseText += "|";
        //            break;
        //        default:
        //            transSet.TryGetValue(c.ToString(), out var morseValue);
        //            morseText += morseValue;
        //            break;
        //    }

        //    //Add two spaces for in between characters.
        //    morseText += "  ";
        //}

        //return morseText;
        return null;
    }
}