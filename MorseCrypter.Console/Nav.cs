using MorseCrypter.Core;

namespace MorseCrypter.ConsoleApp;

public class Nav
{
    public FileReader FileReader { get; set; }
    public Base36ToMorseTranslator TextToMorseTranslator { get; set; }
    public MorseToBase36Translator MorseToTextTranslator { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public Nav()
    {
        //Initialises the translators.
        TextToMorseTranslator = new Base36ToMorseTranslator();
        MorseToTextTranslator = new MorseToBase36Translator();

        //Initialises the file reader with no hard-coded directory.
        FileReader = new FileReader();
    }

    public void Initialise()
    {

        FileReader.Initialise(); //TODO: Restructure this to be after the main menu?



        while (true)
        {
            Console.WriteLine("Main Menu\n");
            Console.WriteLine("1. Translate text to morse code");
            Console.WriteLine("2. Translate morse code to text");
            Console.WriteLine("3. Exit");
            Console.WriteLine("\nPlease enter the number of the option you would like to select:");

            var input = InputValidation.GetUserNumberInput();

            switch (input)
            {
                case 1:
                    StartTranslate();
                    break;

                case 2:
                    break;

                //Exits the program.
                case 3:
                    Environment.Exit(0);
                    break;

                //If the user input is invalid, prompt the user to try again.
                default:
                    Console.WriteLine("Invalid input!");
                    continue;
            }
            break;
        }
    }

    public void StartTranslate()
    {
        //Loads the translation set via a user selection.
        var transSet = FileReader.CharacterSets[FileReader.PrintTranslationSetsToConsole()];

        var input = Console.ReadLine();
        var morse = TextToMorseTranslator.ConvertBase36ToMorse(transSet, input);
        Console.WriteLine(morse);

    }
}