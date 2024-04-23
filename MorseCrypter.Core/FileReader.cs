namespace MorseCrypter.Core;

/// <summary>
/// Provides a set of functions to read the translation sets from provided files.
/// </summary>
public class FileReader
{
    private string LocalFileDirectory { get; set; }
    private List<string> TranslationFiles { get; set; }
    public List<Dictionary<string, string>> CharacterSets { get; private set; }

    public void Initialise()
    {
        if (string.IsNullOrEmpty(LocalFileDirectory))
        {
            LocalFileDirectory = GetUserInputDirectory();
        }

        TranslationFiles = GetListOfTranslationSets(LocalFileDirectory);
        CharacterSets = GetTranslationSets(TranslationFiles);
    }

    /// <summary>
    /// Gets user input for the directory of translation sets.
    /// </summary>
    /// <returns>The user-input directory in which the translation sets are saved.</returns>
    private static string GetUserInputDirectory()
    {
        while (true)
        {
            Console.WriteLine("Please enter the directory of your translation sets:");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && Directory.Exists(input)) return input;
            //If user input is empty or the directory does not exist, prompt the user to try again.
            Console.WriteLine("Invalid input!\nPlease enter the folder the files are contained in.");
        }
    }

    /// <summary>
    /// Returns a list of every txt file containing the line "# Translation Set #".
    /// </summary>
    /// <param name="inputDirectory">The validated user-specified directory of translation sets.</param>
    /// <returns>A list of the names of the files.</returns>
    private static List<string> GetListOfTranslationSets(string inputDirectory)
    {
        while (true)
        {
            //Select every file which contains the line "# Translation Set #".
            List<string> txtFiles = Directory.EnumerateFiles(inputDirectory, "*.txt")
                .Where(txtFile => File.ReadLines(txtFile).Any(line => line.Equals("# Translation Set #")))
                .ToList();

            //If no files contain the specified line.
            if (txtFiles.Count == 0)
            {
                Console.WriteLine("No translation files were found in that directory!\nPress 1 to try again.");
                var input = InputValidation.GetUserNumberInput();

                if (input == 1) continue;

                //If the user does not want to try again, exit the program.
                Environment.Exit(0);
            }
            //Return the list of files.
            return [..txtFiles];
        }
    }

    /// <summary>
    /// Converts each file into a translation set.
    /// </summary>
    /// <param name="files">The files which are tagged as translation sets.</param>
    /// <returns>A list of a dictionary of the translation sets.</returns>
    public static List<Dictionary<string, string>> GetTranslationSets(List<string> files)
    {
        List<Dictionary<string, string>> translationSets = [];
        foreach (var file in files)
        {
            Dictionary<string, string> dict = [];

            var lines = File.ReadAllLines(file);

            foreach (var line in lines)
            {
                //Skip empty lines or lines starting with #.
                if (!line.StartsWith('#') && !string.IsNullOrEmpty(line))
                {
                    //Store the Base36 character.
                    var base36Key = line[0].ToString();

                    //Store the morse translation.
                    //The morse translation is expected to start on the third character of each line.
                    var morseValue = line[2..];

                    //Add the translation set to the dictionary.
                    dict[base36Key] = morseValue;
                }
            }

            //Add the dictionary to the dictionary set list.
            translationSets.Add(dict);
        }

        return translationSets;
    }

    /// <summary>
    /// Prints out the list of available translation sets to the console.
    /// </summary>
    public byte PrintTranslationSetsToConsole()
    {
        Console.WriteLine($"\nThere are {TranslationFiles.Count} translation sets available to translate to.\nPlease select which one you would like to use:");
        for (var i = 0; i < TranslationFiles.Count; i++)
        {
            //Prints out the name of each translation set.
            Console.WriteLine("{0}. {1}", i, Path.GetFileNameWithoutExtension(TranslationFiles[i]).ToUpper());
        }
        var input = InputValidation.GetUserNumberInput();
        return input;
    }
}