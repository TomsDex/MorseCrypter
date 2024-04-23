﻿using MorseCrypter.Core;

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

        FileReader.Initialise(); //TODO: Restructure this to be after the main menu?



        while (true)
        {
            Console.WriteLine("Main Menu\n");
            Console.WriteLine("1. Translate text to morse code");
            Console.WriteLine();
            Console.WriteLine("3. Exit");
            Console.WriteLine("\nPlease enter the number of the option you would like to select:");

            //Stores the user input.
            var keyInfo = Console.ReadKey();
            var input = keyInfo.KeyChar;

            switch (input)
            {
                case '1':
                    StartTranslate();
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

    public void StartTranslate()
    {
        var text = Console.ReadLine();
        var morse = TextToMorseTranslator.ConvertBase36ToMorse(FileReader, text);
        Console.WriteLine(morse);

    }
}