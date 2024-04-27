using System.Text.RegularExpressions;

namespace MorseCrypter.Core;

public static class InputValidation
{

    /// <summary>
    /// Gets a valid user number input from a single key press.
    /// </summary>
    /// <returns>The user input.</returns>
    public static byte GetUserNumberInput()
    {
        while (true)
        {
            //Stores the user key input.
            var keyInfo = Console.ReadKey();
            var input = keyInfo.KeyChar;

            //If the input is a number, return the number.
            if (char.IsNumber(input))
            {
                //Converts the char to a byte.
                return byte.Parse(input.ToString());
            }

            //If the input is not a number, prompt the user to try again.
            Console.WriteLine("Invalid input!\nPlease enter a valid number.");
        }
    }

    /// <summary>
    /// Gets a valid user text input.
    /// </summary>
    /// <returns>The user input.</returns>
    public static string GetUserTextInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input)) return input;
            //If user input is empty, prompt the user to try again.
            Console.WriteLine("Invalid input!\nPlease enter a valid string.");
        }
    }

    /// <summary>
    /// Gets a valid user directory.
    /// </summary>
    /// <param name="research">The program-specified artifact being searched for.</param>
    /// <returns>A valid directory.</returns>
    public static string GetUserDirectoryInput(string research)
    {
        while (true)
        {
            Console.WriteLine("Please enter the directory of {0}:", research);
            string? input;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || !Directory.Exists(input))
                {
                    Console.WriteLine("Invalid input!\nPlease enter a valid directory.");
                }
            } while (string.IsNullOrEmpty(input) || !Directory.Exists(input));

            return input;
        }
    }

    /// <summary>
    /// Gets a valid user single key input.
    /// </summary>
    /// <returns>The key input.</returns>
    public static string GetUserBase36Input()
    {
        while (true)
        {
            //Stores the user key input.
            var keyInfo = Console.ReadKey();
            var input = keyInfo.KeyChar;

            //If the input is base 36 input.
            if (char.IsLetterOrDigit(input))
            {
                return input.ToString().ToUpper();
            }

            //If the input is not a Base 36 input, prompt the user to try again.
            Console.WriteLine("Invalid input!\nPlease enter a valid number.");
        }
    }

    /// <summary>
    /// Gets a valid user Yes/No input.
    /// </summary>
    /// <returns>Y or N depending on the user input.</returns>
    public static string GetUserYNInput()
    {
        while (true)
        {
            //Stores the user key input.
            var keyInfo = Console.ReadKey();
            var input = keyInfo.KeyChar.ToString().ToUpper();

            if (input is "Y" or "N")
            {
                return input;
            }

            Console.WriteLine("Invalid input! Please enter either 'Y' or 'N'.");
       
        }
    }

    /// <summary>
    /// Gets a valid Morse code input. The user may input a ".", "-", or space.
    /// </summary>
    /// <returns>The user-input string.</returns>
    public static string GetUserMorseCodeInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, @"^[.·\s-]+$"))
            {
                var inputWithPeriodConvertedIntoInterpunct = input.Replace(".", "·");
                return inputWithPeriodConvertedIntoInterpunct;
            }
            Console.WriteLine("Invalid input! Please enter valid Morse code.");
        }
    }
}