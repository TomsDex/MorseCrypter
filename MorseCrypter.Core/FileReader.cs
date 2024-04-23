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
        do
        {
            //Initialise the local file directory.
            LocalFileDirectory = InputValidation.GetValidUserDirectory("your translation files");

            //Search for translation files.
            TranslationFiles = GetListOfTranslationSets(LocalFileDirectory);

            //If no translation files are found, prompt the user to try again.
        } while (TranslationFiles.Count == 0);
        
        //Convert the translation files into translation sets.
        CharacterSets = GetTranslationSets(TranslationFiles);
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

            //Return every file which contains the line "# Translation Set #".
            return Directory.EnumerateFiles(inputDirectory, "*.txt")
                .Where(txtFile => File.ReadLines(txtFile).Any(line => line.Equals("# Translation Set #")))
                .ToList();
        }
    }
    

    /// <summary>
    /// Converts each file into a translation set.
    /// </summary>
    /// <param name="files">The files which are tagged as translation sets.</param>
    /// <returns>A list of a dictionary of the translation sets.</returns>
    private static List<Dictionary<string, string>> GetTranslationSets(List<string> files)
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