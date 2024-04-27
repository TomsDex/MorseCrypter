﻿namespace MorseCrypter.Core;

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
                ? "I'll give you a morse code value from your {0} translation set, and you have to give me the letter/number it equates to!"
                : "I'll give you a letter/number from your {0} translation set, and you have to give me the morse code it equates to!",
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

            //Stores the user answer.
            var userAnswer = isMorseToBase36Training
                ? InputValidation.GetUserBase36Input()
                : InputValidation.GetValidMorseCodeInput();


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
                    break;
                }
            }
        }
    }


    /// <summary>
    /// Generates a random Base36 key.
    /// </summary>
    /// <param name="transSet">The user-specified translation set.</param>
    /// <returns>A base 36 key in a random position, as determined by GetRandomNumber.</returns>
    //public string GenerateRandomBase36Key(Dictionary<string, string> transSet)
    //{
    //    return transSet.ElementAt(GenerateRandomNumber(1, transSet.Count)).Key;
    //}

    /// <summary>
    /// Generates a random morse value.
    /// </summary>
    /// <param name="transSet">The user-specified translation set.</param>
    /// <returns>A morse value in a random position, as determined by GetRandomNumber.</returns>
    //public string GenerateRandomMorseValue(Dictionary<string, string> transSet)
    //{
    //    return transSet.ElementAt(GenerateRandomNumber(1, transSet.Count)).Value;
    //}

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