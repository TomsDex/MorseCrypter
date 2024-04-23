namespace MorseCrypter;

public class Nav
{
    public static void Initialise()
    {
        //Initialises the file reader with no hard-coded directory.
        var fileReader = new FileReader.FileReader();
        fileReader.Initialise();

        while (true)
        {
            Console.WriteLine("Main Menu\n");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("3. Exit");
            Console.WriteLine("\nPlease enter the number of the option you would like to select:");

            //Stores the user input.
            var keyInfo = Console.ReadKey();
            var input = keyInfo.KeyChar;

            switch (input)
            {
                case '1':
                    break;

                case '2':
                    break;

                //Exits the program.
                case '3':
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
}