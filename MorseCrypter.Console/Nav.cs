using MorseCrypter.Core;
using System.Text;

namespace MorseCrypter.ConsoleApp;

public class Nav
{
    private FileReader FileReader { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public Nav()
    { 
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
            Console.WriteLine("1. Translate text to Morse code");
            Console.WriteLine("2. Translate Morse code to text");
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
                //Starts the text to Morse code translation.
                case 1:
                    UIStartTranslate(true);
                    break;

                //Starts the Morse code to text translation.
                case 2:
                    UIStartTranslate(false);
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

    public void UIStartTranslate(bool isTextToMorseCode)
    {
        //Allow user to choose a translation set.
        FileReader.PrintTranslationSetsToConsole();
        var transSetChoice = InputValidation.GetUserNumberInput();
        var transSet = FileReader.CharacterSets[transSetChoice];
        Console.Clear();
        //Writes the translation set the user has chosen.
        Console.WriteLine("Translation Set: {0}", 
            FileReader.CharacterSets[transSetChoice].Values.First().ToUpper());

        //Prompt the user to enter their input.
        Console.WriteLine(isTextToMorseCode 
            ? "Enter your text input:" 
            : "Enter your Morse input:");

        //Get the validated user input.
        var userInput = isTextToMorseCode 
            ? InputValidation.GetUserTextInput().ToUpper() 
            : InputValidation.GetUserMorseCodeInput();

        //Start the conversion process.
        var convertedText = isTextToMorseCode 
            ? Spine.UserInputStringToMorseCode(userInput, transSet)
            : Spine.UserInputMorseCodeToString(userInput, transSet);

        Console.WriteLine(convertedText);
    }
}