namespace MorseCrypter.Core
{
    public class InputValidation
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

    }
}
