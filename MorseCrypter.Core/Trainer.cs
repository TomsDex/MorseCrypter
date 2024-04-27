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
            Console.WriteLine("Would you like to translate morse to letters/numbers or letters/numbers to morse?");
            Console.WriteLine("1. Morse to letters/numbers");
            Console.WriteLine("2. Letters/numbers to morse");
            var input = InputValidation.GetUserNumberInput();
            Console.Clear();
            switch (input)
            {
                //Morse to letters/numbers.
                case 1:
                    CommenceTraining(true, transSet);
                    break;
                //Letters/numbers to morse.
                case 2:
                    CommenceTraining(false, transSet);
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    break;

            }
        }
    }

    public void CommenceTraining(bool isMorseToBase36Training, Dictionary<string, string> transSet)
    {
        Console.WriteLine("How to play:");
        Console.WriteLine(
            isMorseToBase36Training
                ? "I'll give you a morse code key from your {0} translation set, and you have to give me the letter/number it equates to!"
                : "I'll give you a letter/number from your {0} translation set, and you have to give me the morse code it equates to!",
            transSet.ElementAt(0).Value);

        var loop = true;

        while (loop)
        {
            
        }
    }


    /// <summary>
    /// Generates a random morse key.
    /// </summary>
    /// <param name="transSet">The user-specified translation set.</param>
    /// <returns>A morse code key in a random position, as determined by GetRandomNumber.</returns>
    public string GenerateRandomKey(Dictionary<string, string> transSet)
    {
        return transSet.ElementAt(GenerateRandomNumber(1, transSet.Count)).Key;
    }

    /// <summary>
    /// Generates a random letter value.
    /// </summary>
    /// <param name="transSet">The user-specified translation set.</param>
    /// <returns>A letter value in a random position, as determined by GetRandomNumber.</returns>
    public string GenerateRandomValue(Dictionary<string, string> transSet)
    {
        return transSet.ElementAt(GenerateRandomNumber(1, transSet.Count)).Value;
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