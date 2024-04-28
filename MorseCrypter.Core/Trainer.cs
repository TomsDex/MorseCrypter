namespace MorseCrypter.Core;

/// <summary>
/// The training functionality.
/// </summary>
public static class Trainer
{

    /// <summary>
    /// The main menu of the training functionality.
    /// </summary>
    /// <param name="transSet">The translation set.</param>
    public static void TrainingMenu(Dictionary<string, string>? transSet)
    {
        Console.Clear();
        Console.WriteLine("Translation Set: {0}", transSet?.Values.First().ToUpper());
        
        Console.WriteLine("Would you like to translate Morse to letters/numbers or letters/numbers to Morse?");
            Console.WriteLine("1. Morse to letters/numbers");
            Console.WriteLine("2. Letters/numbers to Morse");
            var input = InputValidation.GetUserNumberInput();
            Console.Clear();
            switch (input)
            {
                //Morse to letters/numbers.
                case 1:
                    CommenceTraining(true, transSet);
                    break;

                //Letters/numbers to Morse.
                case 2:
                    CommenceTraining(false, transSet);
                    break;

                default:
                    Console.WriteLine("Invalid input!");
                    break;

            }
    }

    /// <summary>
    /// Runs the training functionality.
    /// </summary>
    /// <param name="isMorseToBase36Training">If the user has selected to translate Morse to Base 36.</param>
    /// <param name="transSet">The user-selected translation set.</param>
    private static void CommenceTraining(bool isMorseToBase36Training, Dictionary<string, string>? transSet)
    {
        Console.WriteLine("How to play:");
        if (transSet == null) return;
        Console.WriteLine(
            isMorseToBase36Training
                ? "I'll give you a Morse code value from your {0} translation set, and you have to give me the letter/number it equates to!"
                : "I'll give you a letter/number from your {0} translation set, and you have to give me the Morse code it equates to!",
            transSet.ElementAt(0).Value);

        var streak = 0;

        while (true)
        {
            //Generate a random number between 1 and the length of the translation set.
            //Min value starts at 1 as value 0 is the translation set name.
            var random = GenerateRandomNumber(1, transSet.Count);

            //Stores the question.
            var question = isMorseToBase36Training
                ? transSet.ElementAt(random).Value
                : transSet.ElementAt(random).Key;

            //Stores the actual answer.
            var actualAnswer = isMorseToBase36Training
                ? transSet.ElementAt(random).Key
                : transSet.ElementAt(random).Value;

            Console.WriteLine("Streak: {0}", streak);
            Console.WriteLine("Your question is:\n");
            Console.WriteLine(question + "\n");
            Console.WriteLine("Your answer: ");

            //Stores the user's inputted answer.
            var userAnswer = isMorseToBase36Training
                ? InputValidation.GetUserBase36Input()
                : InputValidation.GetUserMorseCodeInput();


            if (userAnswer == actualAnswer)
            {
                Console.WriteLine("\nCorrect!");
                streak++;
            }
            else
            {
                Console.WriteLine("\nIncorrect! The correct answer was: {0}", actualAnswer);
                Console.WriteLine("Your streak was {0}", streak);
                streak = 0;
                Console.WriteLine("Would you like to play again? Y/N");
                var playAgain = InputValidation.GetUserYesNoInput();

                //If the user wants to play again, restart the training.
                if (playAgain == "Y") CommenceTraining(isMorseToBase36Training, transSet);
                //If the user does not want to play again, return to the training menu.
                else break;
            }
        }
    }

    /// <summary>
    /// Generates a random number between the specified range.
    /// </summary>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>An integer in the specified range.</returns>
    private static int GenerateRandomNumber(int min, int max)
    {
        var random = new Random();
        return random.Next(min, max + 1);
    }
}