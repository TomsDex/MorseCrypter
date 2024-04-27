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
        if (FileReader.CharacterSets == null)
        {
            FileReader.Initialise();
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Main Menu\n");
            Console.WriteLine("0. Change directory of translation files");
            Console.WriteLine("1. Translate text to morse code");
            Console.WriteLine("2. Translate morse code to text");
            Console.WriteLine("3. Do some training");
            Console.WriteLine("4. Exit");
            Console.WriteLine("\nPlease enter the number of the option you would like to select:");

            var input = InputValidation.GetUserNumberInput();

            switch (input)
            {
                //Restarts to change the directory of translation files.
                case 0:
                    Console.Clear();
                    Nav nav = new();
                    nav.Initialise();
                    break;
                //Starts the text to morse code translation.
                case 1:
                    StartTranslate();
                    break;
                //Starts the morse code to text translation.
                case 2:
                    break;
                //Starts the training.
                case 3:
                    Trainer trainer = new(FileReader);
                    trainer.TrainingMenu();
                    Nav newNav = new();
                    newNav.Initialise();
                    break;
                //Exits the program.
                case 4:
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

        FileReader.PrintTranslationSetsToConsole();
        var transSetChoice = InputValidation.GetUserNumberInput();
        var transSet = FileReader.CharacterSets[transSetChoice];

        Console.Clear();
        Console.WriteLine("Translation Set: {0}", FileReader.CharacterSets[transSetChoice].Values.First().ToUpper());
        Console.WriteLine("Enter your text input:");
        //Stores the user input.
        var textInput = InputValidation.GetUserTextInput().ToUpper();

        //Converts the user input to morse code.
        var morse = TextToMorseTranslator.ConvertBase36ToMorse(transSet, textInput);
        Console.WriteLine(morse);

    }
}