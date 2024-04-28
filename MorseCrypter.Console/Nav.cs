using MorseCrypter.Core;

namespace MorseCrypter.ConsoleApp;

/// <summary>
/// The navigation class that allows the user to navigate through the UI of the program.
/// </summary>
public class Nav
{
    public static string localFileDirectory = InputValidation.GetUserDirectoryInput("your local translation files");
    public static List<string> translationFiles = FileReader.GetListOfTranslationSets(localFileDirectory);
    public static List<Dictionary<string, string>> translationSets = FileReader.GetTranslationSets(translationFiles);
    
    /// <summary>
    /// Initialises the navigation menu.
    /// </summary>
    public static void Initialise()
    {
        while (true)
        {
            Console.Clear();
            Console.Write("Main Menu\n");
            Console.WriteLine("1. Translate text to Morse code");
            Console.WriteLine("2. Translate Morse code to text");
            Console.WriteLine("3. Do some training");
            Console.WriteLine("4. Exit");
            Console.WriteLine("\nPlease enter the number of the option you would like to select:");

            var input = InputValidation.GetUserNumberInput();

            switch (input)
            {
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
                    StartTraining();
                    break;

                //Exits the program.
                case 4:
                    Environment.Exit(0);
                    break;

                //If the user input is invalid, prompt the user to try again.
                default:
                    Console.WriteLine("Invalid input! Please enter 1-4.");
                    continue;
            }
            break;
        }
    }

    /// <summary>
    /// Starts the translation process.
    /// </summary>
    /// <param name="isTextToMorseCode">A hardcoded value to determine which way the translation process happens.</param>
    public static void UIStartTranslate(bool isTextToMorseCode)
    {
        //Allow user to choose a translation set.
        var transSet = ChosenTranslationSet();

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

        //Outputs the final result.
        Console.WriteLine(convertedText);
    }

    /// <summary>
    /// Starts the training process.
    /// </summary>
    public static void StartTraining()
    {
        var transSet = ChosenTranslationSet();
        Trainer.TrainingMenu(transSet);
    }

    /// <summary>
    /// Chooses a translation set.
    /// </summary>
    /// <returns>The chosen translation set.</returns>
    public static Dictionary<string, string> ChosenTranslationSet()
    {
        FileReader.PrintTranslationSetsToConsole(translationFiles);
        var transSetChoice = InputValidation.GetUserNumberInput();
        Console.Clear();
        return translationSets[transSetChoice];
    }
}