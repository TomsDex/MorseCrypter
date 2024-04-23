namespace FileReader;

/// <summary>
/// Provides a set of functions to read the translation sets from provided files.
/// </summary>
public class FileReader
{
    public string? LocalFileDirectory { get; set; }
    public List<string>? TranslationFiles { get; set; }
    public List<Dictionary<string, string>>? CharacterSets { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public FileReader()
    {
    }

    /// <summary>
    /// Override constructor containing a hard-coded directory.
    /// </summary>
    /// <param name="localFileDirectory">The hard-coded user directory.</param>
    public FileReader(string? localFileDirectory)
    {
        LocalFileDirectory = localFileDirectory;
    }

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
    public static string GetUserInputDirectory()
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
    /// <param name="inputDirectory">The user-specified directory of translation sets.</param>
    /// <returns>A list of the names of the files.</returns>
    public static List<string>? GetListOfTranslationSets(string? inputDirectory)
    {
        //Selects every file which contains the line "# Translation Set #".
        if (inputDirectory != null)
        {
            List<string> txtFiles = Directory.EnumerateFiles(inputDirectory, "*.txt")
                .Where(txtFile => File.ReadLines(txtFile).Any(line => line.Equals("# Translation Set #")))
                .ToList();

            //Return null if no files contain the specified line.
            if (txtFiles.Count == 0)
            {
                return null;
            }
            return new List<string>(txtFiles);
        }

        return null;
    }

    /// <summary>
    /// Converts each file into a translation set.
    /// </summary>
    /// <param name="files">The files which are tagged as translation sets.</param>
    /// <returns>A list of a dictionary of the translation sets.</returns>
    public static List<Dictionary<string, string>>? GetTranslationSets(List<string>? files)
    {
        if (files == null) { return null; }
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
                    string base36Key = line[0].ToString();

                    //Store the morse translation.
                    //The morse translation is expected to start on the third character of each line.
                    string morseValue = line[2..];

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
    /// Used for testing only.
    /// </summary>
    public void WriteToConsole()
    {
        if (TranslationFiles == null || CharacterSets == null) 
        {
            Console.WriteLine(".");
            return;
        }
        Console.WriteLine($"There are {TranslationFiles.Count} translation sets.");
        for (int i = 0; i < TranslationFiles.Count; i++)
        {
            Console.WriteLine($"Translation set {TranslationFiles[i]} is as the following");
            foreach (var line in CharacterSets[i])
            {
                Console.WriteLine(line);
            }
        }
    }
}