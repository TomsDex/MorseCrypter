namespace Translator;

public class Base36ToMorseTranslator
{
    public string? Base36Text { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public Base36ToMorseTranslator()
    {

    }

    public void ConvertBase36ToMorse(string? base36Text)
    {
        Base36Text = base36Text;
    }
}