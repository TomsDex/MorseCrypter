namespace MorseCrypter.Core;

public class Trainer()
{
    public FileReader FileReader { get; set; }

    public Trainer(FileReader fileReader) : this()
    {
        FileReader = fileReader;
    }

    public void TrainingMenu()
    {
        FileReader.PrintTranslationSetsToConsole();
        var transSetChoice = InputValidation.GetUserNumberInput();
        var transSet = FileReader.CharacterSets[transSetChoice];

        Console.Clear();
        Console.Write("Translation Set: {0}", FileReader.CharacterSets[transSetChoice].Values.First().ToUpper());
        while (true)
        {
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
    }

    /// <summary>
    /// Runs the training functionality.
    /// </summary>
    /// <param name="isMorseToBase36Training">If the user has selected to translate Morse to Base 36.</param>
    /// <param name="transSet">The user-selected translation set.</param>
    public void CommenceTraining(bool isMorseToBase36Training, Dictionary<string, string> transSet)
    {
        Console.WriteLine("How to play:");
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
            Console.WriteLine("Your question is:\n\n");
            Console.WriteLine(question + "\n");
            Console.WriteLine("Your answer: ");

            //Stores the user's inputted answer.
            var userAnswer = isMorseToBase36Training
                ? InputValidation.GetUserBase36Input()
                : InputValidation.GetUserMorseCodeInput();


            if (userAnswer == actualAnswer)
            {
                Console.WriteLine("Correct!");
                streak++;
            }
            else
            {
                Console.WriteLine("Incorrect! The correct answer was: {0}", actualAnswer);
                Console.WriteLine("Your streak was {0}", streak);
                streak = 0;
                Console.WriteLine("Would you like to play again? Y/N");
                var playAgain = InputValidation.GetUserYNInput();
                if (playAgain == "Y")
                {
                    break; //TODO
                }
            }
        }
    }

    /// <summary>
    /// Generates a random number between the specified range.
    /// </summary>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>An integer in the specified range.</returns>
    private int GenerateRandomNumber(int min, int max)
    {
        var random = new Random();
        return random.Next(min, max + 1);
    }
}