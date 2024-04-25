namespace MorseCrypter.Core;

public class Base36ToMorseTranslator
{
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

    public string ConvertMorseToBase36(Dictionary<string, string> transSet, string morseText)
    {
        var base36Text = string.Empty;

        foreach (var c in morseText)
        {
            switch (c)
            {
                case ' ':
                    //TODO: Check if a second space - if so ignore
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